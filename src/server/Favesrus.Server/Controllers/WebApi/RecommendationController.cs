﻿using Favesrus.DAL.Impl;
using Favesrus.Model.Entity;
using Favesrus.Server.Dto.GiftItem;
using Favesrus.Server.Filters;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Models.Recommendation;
using Favesrus.Server.Processing;
using Favesrus.Server.Processing.Interface;
using Favesrus.Services;
using Favesrus.Services.Interfaces;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.Server.Controllers.WebApi
{
    //[Authorize]
    [RoutePrefix("api/recommendation")]
    public class RecommendationController : ApiBaseController
    {
        private FavesrusDbContext db = new FavesrusDbContext();
        IAutoMapper _mapper;
        IRecommendationsProcessor _recommendationsProcessor;

        public RecommendationController(IAutoMapper mapper, 
            IRecommendationsProcessor recommendationProcessor)
        {
            _mapper = mapper;
            _recommendationsProcessor = recommendationProcessor;
        }

        public RecommendationController(IAutoMapper mapper,
                IRecommendationsProcessor recommendationProcessor,
                FavesrusUserManager userManager,
                FavesrusRoleManager roleManager,
                IAuthenticationManager authManager)
            : base(userManager, roleManager, authManager)
        {
            _mapper = mapper;
            _recommendationsProcessor = recommendationProcessor;
        }

        
        // Gets all the possible recommendations in our database
        [HttpGet]
        [Route("getrecommendations")]
        public async Task<IHttpActionResult>  GetRecommendations(HttpRequestMessage requestMessage)
        {
            string successStatus = "get_recommendations_success";
            string successMessage = "Successfully retrieved recommendations list";

            var recommendations = db.Recommendations.ToList();

            return new BaseActionResult<ICollection<Recommendation>>(
                requestMessage,
                recommendations,
                successMessage,
                successStatus);
        }

    }
}