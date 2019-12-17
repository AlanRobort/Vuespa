using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Service;
using Service.extend;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webapi.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/ClientInfo/[controller]")]
    [ApiController]
    public class AccessInfoController : ControllerBase
    {
        private readonly IClientInfoService _clientInfoService;
        private readonly IUrlHelper _urlHelper;

        public AccessInfoController(
            IClientInfoService clientInfoService,
            IUrlHelper urlHelper
            )
        {
            _clientInfoService = clientInfoService;
            _urlHelper = urlHelper;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var result = await _clientInfoService.GetAll();
        //    return Ok(result);
        //}

        [HttpGet("GetIdnumResult/{id}/{num}")]
        public async Task<IActionResult> GetIdnumResult(int id,int num)
        {
            var result = await _clientInfoService.GetClientAddressInfos(id, num);
            return Ok(result);
        }


        [HttpGet("GetAccessinfoAll")]
        public async Task<IActionResult> GetAccessinfoAll([FromQuery] PaginationParamer paginationParamer) 
        {
            //PaginationParamer paginationParamer = new PaginationParamer();
            //paginationParamer.PageIndex = PageIndex;
            //paginationParamer.PageSize = PageSize;
            var AccessinfoList = await _clientInfoService.GetAllAddressInfo(paginationParamer);
            var previousParameter = AccessinfoList.HasPrevious
                ? CreateAccessinfoUrl(paginationParamer, PaginationResourceUrlType.PreviousPage):null;
            var nextPatameter = AccessinfoList.HasNext
                ? CreateAccessinfoUrl(paginationParamer, PaginationResourceUrlType.NextPage) : null;

            var meta = new
            {
                AccessinfoList.TotalItemCount,
                AccessinfoList.paginationBase.PageIndex,
                AccessinfoList.paginationBase.PageSize,
                AccessinfoList.PageCount,
                previouslink = previousParameter,
                nextlink = nextPatameter
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta));

          return Ok(AccessinfoList);
        }


        private string CreateAccessinfoUrl(PaginationParamer paginationParamer, PaginationResourceUrlType paginationResourceUrlType)
        {
            switch (paginationResourceUrlType)
            {
                case PaginationResourceUrlType.PreviousPage:
                    var previousParameters = new
                    {
                        PageIndex = paginationParamer.PageIndex-1,
                        pageSize = paginationParamer.PageSize
                        //OrderBy=paginationParamer.OrderBy
                    };
                    return _urlHelper.Link("GetAccessinfo", previousParameters);
                case PaginationResourceUrlType.NextPage:
                    var NextPageParameters = new
                    {
                        PageIndex = paginationParamer.PageIndex+1,
                        pageSize = paginationParamer.PageSize
                        //OrderBy=paginationParamer.OrderBy
                    };
                    return _urlHelper.Link("GetAccessinfo", NextPageParameters);
                case PaginationResourceUrlType.CurrentPage:
                default:
                    var CurrentPageParameters = new
                    {
                        PageIndex = paginationParamer.PageIndex,
                        pageSize = paginationParamer.PageSize
                        //OrderBy=paginationParamer.OrderBy
                    };
                    return _urlHelper.Link("GetAccessinfo", CurrentPageParameters);
               
            }
        }

        
    }
}