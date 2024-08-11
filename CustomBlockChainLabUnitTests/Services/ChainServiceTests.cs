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
    public void should_generate_new_block()
    {
        GivenBlock(new BlockDomain
        {
            Hash = "123"
        });
        var timeStamp = DateTime.Now;
        var hashRawData = $"{timeStamp}:123:data:{0}";

        var block = _chainService.GenerateNewBlock(new GenerateNewBlockDto
        {
            TimeStamp = timeStamp,
            Data = "data"
        });

        block.PreviousHash.Should().Be("123");
        block.Hash.Should().Be(HashHelper.ToSha256(hashRawData));
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

    private void GivenBlock(BlockDomain blockDomain)
    {
        _chainRepository?.GetBlockBy(Arg.Any<int>()).Returns(blockDomain);
    }
}