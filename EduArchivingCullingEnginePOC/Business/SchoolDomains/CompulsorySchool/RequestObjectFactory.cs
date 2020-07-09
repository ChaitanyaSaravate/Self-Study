using System;
using System.Collections.Generic;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Interfaces;
using Business.SchoolDomains.KAA;
using Microsoft.Extensions.DependencyInjection;

namespace Business.SchoolDomains.CompulsorySchool
{
    // TODO: Make it generic so that every school domain wont have to do this.
    public class RequestObjectFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestObjectFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IDomainHandler GetArchiveHandler(SupportedEduEntityTypes eduEntityType)
        {
            switch (eduEntityType)
            {
                case SupportedEduEntityTypes.Student:
                    return _serviceProvider.GetService<CompulsorySchoolDomainHandlerHandler>();
                case SupportedEduEntityTypes.Grades:
                    return _serviceProvider.GetService<KaaDomainHandlerHandler>();
                default:
                    throw new KeyNotFoundException();

            }
        }
    }
}
