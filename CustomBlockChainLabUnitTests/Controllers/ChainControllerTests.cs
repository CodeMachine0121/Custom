using CustomBlockChainLab;
using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Models.Http;
using CustomBlockChainLab.Services.Interfaces;
using FluentAssertions;
using NSubstitute;

namespace CustomBlockChainLabUnitTests.Controllers;

[TestFixture]
public class ChainControllerTests
{
    private ChainController _chainController;
    private IChainService? _chainService;

    [SetUp]
    public void SetUp()
    {
        _chainService = Substitute.For<IChainService>();
        _chainController = new ChainController(_chainService);
    }

    [Test]
    public async Task should_be_ok()
    {
        var response = await _chainController.GetBlockById(1);
        response.Status.Should().Be(ResponseStatus.Ok);
    }

    [Test]
    public async Task should_get_block_by_service()
    {
        await _chainController.GetBlockById(1);
        await _chainService.Received()!.GetBlockById(Arg.Any<int>());
    }

    [Test]
    public async Task should_get_response_with_block()
    {
        var timeStamp = DateTime.Now;
        _chainService!.GetBlockById(Arg.Any<int>()).Returns(new BlockDomain()
        {
            Data = "data",
            Hash = "any-hash",
            PreviousHash = "any-previous-hash",
            TimeStamp = timeStamp,
            Nonce = 0
        });
        
        var response = await _chainController.GetBlockById(1);
        
        response.Data.Should().BeEquivalentTo(new BlockDomain()
        {
            Data = "data",
            Hash = "any-hash",
            PreviousHash = "any-previous-hash",
            TimeStamp = timeStamp,
            Nonce = 0
        });
        
    }

    [Test]
    public async Task should_generate_new_block()
    {
        _chainService!.GenerateNewBlock(Arg.Any<GenerateNewBlockDto>()).Returns(new BlockDomain());
        
        var response = await _chainController.GenerateNewBlock(new GenerateNewBlockRequest()
        {
            Data = "data"
        });
        
        _chainService.Received()?.GenerateNewBlock(Arg.Any<GenerateNewBlockDto>());
        response.Data.GetType().Should().Be(typeof(BlockDomain));
    }
}