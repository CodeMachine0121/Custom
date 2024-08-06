using CustomBlockChainLab;
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
    public void should_be_ok()
    {
        var response = _chainController.GetBlockById(1);
        response.Status.Should().Be(ResponseStatus.Ok);
    }

    [Test]
    public void should_get_block_by_service()
    {
        _chainController.GetBlockById(1);
        _chainService.Received()!.GetBlockById(Arg.Any<int>());
    }
}