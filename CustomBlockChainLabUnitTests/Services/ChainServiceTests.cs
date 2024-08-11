using CustomBlockChainLab.Helpers;
using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services;
using FluentAssertions;
using NSubstitute;

namespace CustomBlockChainLabUnitTests.Services;

[TestFixture]
public class ChainServiceTests
{
    private IChainRepository? _chainRepository;
    private ChainService _chainService;

    [SetUp]
    public void SetUp()
    {
        _chainRepository = Substitute.For<IChainRepository>();
        _chainService = new ChainService(_chainRepository);
    }

    [Test]
    public void should_get_block_by_repo()
    {
        GivenBlock(new BlockDomain
        {
            Id = 1
        });

        var block = _chainService.GetBlockById(1);

        block.Id.Should().Be(1);
    }

    [Test]
    public void should_insert_new_block_to_chain()
    {
        GivenBlock(new BlockDomain
        {
            Hash = "123"
        });

        _chainService.GenerateNewBlock(new GenerateNewBlockDto
        {
            Data = "",
            TimeStamp = DateTime.Now
        });

        _chainRepository.Received()!.InsertBlock(Arg.Any<BlockDomain>());
    }

    [Test]
    public async Task should_generate_new_block_base_on_latest_block()
    {
        _chainRepository!.GetChainLength().Returns(0);
        await _chainService.GenerateNewBlock(new GenerateNewBlockDto()
        {
            Data = "new",
            TimeStamp = DateTime.Now
        });

        _chainRepository.DidNotReceive().GetBlockBy(Arg.Any<int>());
        await _chainRepository.Received().InsertBlock(Arg.Is<BlockDomain>(b => b.Data == "Genesis Block"));
    }

    private void GivenBlock(BlockDomain blockDomain)
    {
        _chainRepository?.GetBlockBy(Arg.Any<int>()).Returns(blockDomain);
    }
}