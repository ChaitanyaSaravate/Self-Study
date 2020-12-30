export class SelectionDefinition{
    title: string;
    operationType: string;
    schoolType: string;
    status: string;
    executedBy: string;
    executionTime: Date;
}

export class SelectionDefinitionEntity
    {
        Id: number;
        Title: string;
        SupportedOperationType: string;
        EduEntityGroup: string;
        SchoolDomain: string;
        EduEntity: EduEntity[];   
    }


    export class EduEntity
    {
        Id: number;
        Name: string;
        ParentEntity: string;
        EntityType: string;
        ArchiveEndpoints: string[];
        CullingEndpoints: string[];       
    }