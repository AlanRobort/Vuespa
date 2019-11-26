using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;
using Service.extend;

namespace Webapi.Controllers
{
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


        [HttpGet]
        public async Task<IEnumerable<ClientAddressInfo>> GetAccessinfoAll([FromQuery] PaginationParamer paginationParamer) 
        {
            //PaginationParamer paginationParamer = new PaginationParamer();
            //paginationParamer.PageIndex = PageIndex;
            //paginationParamer.PageSize = PageSize;
            var Accessinfo = await _clientInfoService.GetAllAddressInfo(paginationParamer);
          return Accessinfo;
        }

        //private string CreateAccessinfoUrl(PaginationParamer paginationParamer, PaginationResourceUrlType paginationResourceUrlType) 
        //{
        //    switch (paginationResourceUrlType) 
        //    {
        //        case PaginationResourceUrlType.PreviousPage:
        //            var previousParameters = new
        //            {
        //                PageIndex = paginationParamer.PageIndex,
        //                pageSize = paginationParamer.PageSize
        //                //OrderBy=paginationParamer.OrderBy
        //            };
        //            return _urlHelper.Link("GetCountries", previousParameters);
        //        case PaginationResourceUrlType.NextPage:
        //            var NextPageParameters = new
        //            {
        //                PageIndex = paginationParamer.PageIndex,
        //                pageSize = paginationParamer.PageSize
        //                //OrderBy=paginationParamer.OrderBy
        //            };
        //            return _urlHelper.Link("GetCountries", NextPageParameters);
        //        case PaginationResourceUrlType.CurrentPage:
        //            var CurrentPageParameters = new
        //            {
        //                PageIndex = paginationParamer.PageIndex,
        //                pageSize = paginationParamer.PageSize
        //                //OrderBy=paginationParamer.OrderBy
        //            };
        //            return _urlHelper.Link("GetCountries", CurrentPageParameters);
        //    }
        //}
    }
}