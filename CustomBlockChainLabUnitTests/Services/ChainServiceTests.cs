using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services;
using EccSDK.models.ChameleonHash;
using EccSDK.Models.ChameleonHash;
using EccSDK.models.Keys;
using EccSDK.Services.Interfaces;
using FluentAssertions;
using NSubstitute;

namespace CustomBlockChainLabUnitTests.Services;

[TestFixture]
public class ChainServiceTests
{
    private IChainRepository? _chainRepository;
    private ChainService _chainService;
    private KeyPairDomain _keyPairDomain;
    private IChameleonHashService _chameleonHashService;

    [SetUp]
    public void SetUp()
    {
        _chainRepository = Substitute.For<IChainRepository>();
        _keyPairDomain = Substitute.For<KeyPairDomain>();
        _chameleonHashService = Substitute.For<IChameleonHashService>();
        _chainService = new ChainService(_chainRepository);
    }

    [Test]
    public async Task should_insert_new_block_to_chain()
    {
        GivenBlock(new BlockDomain
        {
            Hash = "123"
        });
        GivenChameleonHash();

       await _chainService.GenerateNewBlock(new GenerateNewBlockDto
        {
            Data = "",
            TimeStamp = DateTime.Now
        });

        await _chainRepository.Received()!.InsertBlock(Arg.Any<BlockDomain>());
    }

    [Test]
    public async Task should_not_request_block_when_the_length_is_0()
    {
        _chainRepository!.GetChainLength().Returns(0);
        GivenChameleonHash();
        
        await _chainService.GenerateNewBlock(new GenerateNewBlockDto()
        {
            Data = "new",
            TimeStamp = DateTime.Now
        });

        await _chainRepository.DidNotReceive().GetBlockBy(Arg.Any<int>());
        await _chainRepository.Received().InsertBlock(Arg.Is<BlockDomain>(b => b.Data == "Genesis Block"));
    }

    [Test]
    public async Task should_generate_new_block_base_on_latest_block()
    {
        _chainRepository!.GetChainLength().Returns(1);
        GivenChameleonHash();

        GivenBlock(new BlockDomain
        {
            Hash = "123",
        });

        var newBlock = await _chainService.GenerateNewBlock(new GenerateNewBlockDto()
        {
            Data = "new",
            TimeStamp = DateTime.Now
        });

        await _chainRepository.Received().GetBlockBy(Arg.Is<int>(i => i==1));
        newBlock.PreviousHash.Should().Be("123");
    }

    private void GivenChameleonHash()
    {
        _chameleonHashService.CalculateChameleonHashBy(Arg.Any<ChameleonHashRequest>()).Returns(new ChameleonHash());
    }

    private void GivenBlock(BlockDomain blockDomain)
    {
        _chainRepository?.GetBlockBy(Arg.Any<int>()).Returns(blockDomain);
    }
}