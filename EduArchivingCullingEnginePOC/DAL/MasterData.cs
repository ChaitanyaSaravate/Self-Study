using System.Collections.Generic;
using System.Linq;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;

namespace DAL
{
    public class MasterData
    {
        public static IList<EduEntityGroup> GetEduEntityGroups()
        {
            List<EduEntityGroup> entityGroups = new List<EduEntityGroup>();

            var studentEntity = new EduEntity
            {
                Id = 1,
                Name = "Student",
                EntityType = SupportedEduEntityTypes.Student,
                ArchiveEndpoints = new List<string> { "students/studentinfo" },
                CullingEndpoints = new List<string> { "students/studentinfo" }
            };

            var gradesEntity = new EduEntity
            {
                Id = 2,
                Name = "Grades",
                EntityType = SupportedEduEntityTypes.Grade,
                ArchiveEndpoints = new List<string> { "students/grades" },
                CullingEndpoints = new List<string> { "students/grades" }
            };

            var absencseEntity = new EduEntity
            {
                Id = 3,
                Name = "Absence",
                EntityType = SupportedEduEntityTypes.Absence,
                ArchiveEndpoints = new List<string> { "students/absences" },
                CullingEndpoints = new List<string> { "students/absences" }
            };

            // Group where individual object should be saved in separate file.
            entityGroups.Add(new EduEntityGroup
            {
                SchoolDomain = SupportedSchoolDomains.CompulsorySchool,
                EntityGroup = SupportedEduEntityGroups.Student,
                PerformOperationOnGroupLevel = false,
                Entities = new List<EduEntity>
                {
                    gradesEntity,
                    studentEntity,
                    absencseEntity
                }
            });

            var measuresEntity = new EduEntity
            {
                Id = 1,
                Name = "Measures",
                EntityType = SupportedEduEntityTypes.Measures,
                ArchiveEndpoints = new List<string> { "kaa/measures" },
                CullingEndpoints = new List<string> { "kaa/measures" }
            };

            var remindersEntity = new EduEntity
            {
                Id = 2,
                Name = "Reminders",
                EntityType = SupportedEduEntityTypes.Reminders,
                ArchiveEndpoints = new List<string> { "kaa/reminders" },
                CullingEndpoints = new List<string> { "kaa/reminders" }
            };

            // Group where all objects should be saved in one file.
            entityGroups.Add(new EduEntityGroup
            {
                SchoolDomain = SupportedSchoolDomains.KAA,
                EntityGroup = SupportedEduEntityGroups.KAA,
                PerformOperationOnGroupLevel = true,
                Entities = new List<EduEntity>
                {
                    measuresEntity,
                    remindersEntity
                }
            });

            return entityGroups;
        }

        public static EduEntityGroup GetEduEntityGroup(SupportedEduEntityGroups entityGroup)
        {
            return GetEduEntityGroups().First(e => e.EntityGroup == entityGroup);
        }

        public static List<SelectionDefinition> GetSelectionDefinitions()
        {
            List<SelectionDefinition> selectionDefinitions = new List<SelectionDefinition>();
            var compulsoryStudentSelection = new SelectionDefinition
            {
                Id = 1,
                Title = "CSSelectionDefinition",
                SupportedOperationType = SupportedOperationType.Archive,
                EntitiesToArchiveCull = new List<EduEntity>(),
                EduEntityGroup = SupportedEduEntityGroups.Student
            };

            var entityGroup = MasterData.GetEduEntityGroup(compulsoryStudentSelection.EduEntityGroup);

            foreach (var entity in entityGroup.Entities)
            {
               compulsoryStudentSelection.EntitiesToArchiveCull.Add(entity);
               //ToDO: Remove this break when you want to add more than one entities.
               //break;
            }
            compulsoryStudentSelection.SchoolDomain = entityGroup.SchoolDomain;

            selectionDefinitions.Add(compulsoryStudentSelection);

            var kaaSelection = new SelectionDefinition
            {
                Id = 2,
                Title = "KAASelectionDefinition",
                SupportedOperationType = SupportedOperationType.Archive,
                EntitiesToArchiveCull = new List<EduEntity>(),
                EduEntityGroup = SupportedEduEntityGroups.KAA
            };

            var kaaEntityGroup = MasterData.GetEduEntityGroup(kaaSelection.EduEntityGroup);

            foreach (var entity in kaaEntityGroup.Entities)
            {
                kaaSelection.EntitiesToArchiveCull.Add(entity);
            }
            kaaSelection.SchoolDomain = kaaEntityGroup.SchoolDomain;

            selectionDefinitions.Add(kaaSelection);

            return selectionDefinitions;
        }

        public static SelectionDefinition GetSelectionDefinition(int id)
        {
            return GetSelectionDefinitions().Find(s => s.Id == id);
        }
    }
}
