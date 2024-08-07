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
        _chainRepository!.GetBlockBy(Arg.Any<int>()).Returns(new Block
        {
            Id = 1,
        });
        
        var block = _chainService.GetBlockById(1);
        
        block.Id.Should().Be(1);
    }

    [Test]
    public void should_generate_new_block()
    {
        _chainRepository?.GetBlockBy(0).Returns(new Block
        {
            Hash = "123",
        });
        
        var block = _chainService.GenerateNewBlock(new GenerateNewBlockDto
        {
            Data = "data"
        });
        
        block.PreviousHash.Should().Be("123");
    }
}