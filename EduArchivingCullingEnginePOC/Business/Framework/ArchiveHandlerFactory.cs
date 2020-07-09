using System;
using System.Collections.Generic;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Interfaces;
using Business.SchoolDomains.CompulsorySchool;
using Business.SchoolDomains.KAA;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Framework
{
    //TODO: Try to get rid of factory and try better DI containers which can register "named" services.
    public class ArchiveHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ArchiveHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDomainHandler GetArchiveHandler(SupportedSchoolDomains schoolDomain)
        {
            //TODO: Is named registration of services using some other DI containers a better option?
            switch (schoolDomain)
            {
                case SupportedSchoolDomains.CompulsorySchool:
                    return _serviceProvider.GetService<CompulsorySchoolDomainHandlerHandler>();
                case SupportedSchoolDomains.KAA:
                    return _serviceProvider.GetService<KaaDomainHandlerHandler>();
                default:
                    throw new KeyNotFoundException();

            }
        }
    }
}
