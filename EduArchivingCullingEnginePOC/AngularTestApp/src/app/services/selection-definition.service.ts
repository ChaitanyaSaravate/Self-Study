import { Injectable } from '@angular/core';
import { SelectionDefinition, SelectionDefinitionEntity } from '../models/SelectionDefinitionModel';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SelectionDefinitionService {

  internalApiUrl: URL;
  jsonResponse: string;
  selectionDefs : SelectionDefinitionEntity[];
  constructor(private httpClient: HttpClient) { 
    this.internalApiUrl = new URL("http://localhost:61675/");
  }

  getAll(): SelectionDefinition[]
  {
        let selectionsResponse = this.httpClient.get<SelectionDefinitionEntity[]>(this.internalApiUrl + "selections", { observe: 'response'}).pipe(
      catchError(this.handleError)
    );
    
    var selections: SelectionDefinition[] = [];

    selectionsResponse.subscribe(data => {
      this.jsonResponse = data.toString();
      this.selectionDefs = data.body;
      if(this.selectionDefs.length > 0)
      {

        this.selectionDefs.forEach(element => {
          selections.push(
            {title: element.Title, schoolType: element.SchoolDomain, operationType: element.SupportedOperationType, status: "Draft", executedBy:"Test User", executionTime:null},
          );          
        });

      }
    }, this.handleError);

    // const selectionDefs: SelectionDefinition[] = [
    //   {title: "Test 1", status:"Draft", schoolType: "KAA", executedBy: null, executionTime:null, operationType:"Archiving"},
    //   {title: "Test 1", status:"Draft", schoolType: "KAA", executedBy: null, executionTime:null, operationType:"Archiving"},
    //   {title: "Test 1", status:"Draft", schoolType: "KAA", executedBy: null, executionTime:null, operationType:"Archiving"},
    //   {title: "Test 1", status:"Draft", schoolType: "KAA", executedBy: null, executionTime:null, operationType:"Archiving"},
    // ];

    return selections;
  }

  private handleError(err: HttpErrorResponse)
  {
    if(err.error instanceof ErrorEvent)
    {
      console.error("An error occured", err.message);
    }

    return throwError(err);
  }
}
