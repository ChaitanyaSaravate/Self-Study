using System;
using System.Collections.Generic;
using System.Text;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;
using Business.SchoolDomains.CompulsorySchool;
using Business.SchoolDomains.KAA;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Framework
{
    public class ArchiveHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ArchiveHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IArchive GetArchiveHandler(SupportedSchoolDomains schoolDomain)
        {
            //TODO: Is named registration of services using some other DI containers a better option?
            switch (schoolDomain)
            {
                case SupportedSchoolDomains.CompulsorySchool:
                    return _serviceProvider.GetService<CompulsorySchoolArchiveHandler>();
                case SupportedSchoolDomains.KAA:
                    return _serviceProvider.GetService<KAAArchiveHandler>();
                default:
                    throw new KeyNotFoundException();

            }
        }
    }
}
