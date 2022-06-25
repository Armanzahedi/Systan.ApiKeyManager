using Microsoft.AspNetCore.Mvc;
using Systan.ApiKeyManager.Core.Entities;
using Systan.ApiKeyManager.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Systan.ApiKeyManager.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ConsumersController : ControllerBase
    //{
    //    private readonly IConsumerRepository _consumerRepo;
    //    private readonly IGatewayService _gatewayService;

    //    public ConsumersController(IConsumerRepository consumerRepo, IGatewayService gatewayService)
    //    {
    //        _consumerRepo = consumerRepo;
    //        _gatewayService = gatewayService;
    //    }

    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Consumer>>> Get()
    //    {
    //        var id = Guid.NewGuid().ToString();
    //        await _gatewayService.AddApiKey(new Core.Dtos.GatewayDtos.AddApiKeyRequest
    //        {
    //            id= id,
    //            key = "test",
    //        });
    //        await _gatewayService.UpdateApiKey(id,new Core.Dtos.GatewayDtos.UpdateApiKeyRequest
    //        {
    //            key = "test2",
    //        });
    //        await _gatewayService.DeleteApiKey(id);
    //        return Ok(await _consumerRepo.GetAll());
    //    }

    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Consumer>> Get(int id)
    //    {
    //        return Ok(await _consumerRepo.GetById(id));
    //    }        

    //    [HttpPost]
    //    public async Task Post([FromBody] Create model)
    //    {
    //        await _consumerRepo.Add(new Consumer { SystanId = model.ConsumerId });
    //    }

    //    [HttpPut("{id}")]
    //    public async Task Put(int id, [FromBody] Create model)
    //    {
    //        await _consumerRepo.Update(new Consumer {
    //            Id = id,
    //            SystanId = model.ConsumerId });
    //    }

    //    [HttpDelete("{id}")]
    //    public async Task Delete(int id)
    //    {
    //        await _consumerRepo.Remove(id);
    //    }
    //}
    //public class Create
    //{
    //    public string ConsumerId { get; set; } = null!;
    //}
}
