namespace Abstractions.Internal.Framework
{
    public enum SupportedOperationType
    {
        Archive,
        Cull
    }

    public enum SupportedEduEntityTypes
    {
        Student,
        Grades,
        Absence,
        Youth,
        Measures,
        Reminders
    }

    public enum SupportedEduEntityGroups
    {
        Student,
        KAA
    }

    public enum SupportedSchoolDomains
    {
        CompulsorySchool,
        KAA
    }

    public enum ArchiveStatuses
    {
        DataDownloadInProgress,
        ArchiveFileCreationInProgress,
        DataDownloaded,
        ArchiveFilesCreated,
        InProgress,
        Successful,
        Failed
    }
}
