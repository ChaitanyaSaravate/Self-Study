import { Component, OnInit } from '@angular/core';
import { SelectionDefinition } from '../models/SelectionDefinitionModel';
import { SelectionDefinitionService } from '../services/selection-definition.service';

@Component({
  selector: 'app-selection-def-overview',
  templateUrl: './selection-def-overview.component.html',
  styleUrls: ['./selection-def-overview.component.scss']
})
export class SelectionDefOverviewComponent implements OnInit {
  displayedColumns: string[] = ['title', 'operationType', 'schoolType', 'status', 'executedBy', 'executionTime'];
  selectionDefinitions: SelectionDefinition[];
  
  constructor(private selectionDefService: SelectionDefinitionService) { 

  }

  ngOnInit(): void {
    this.selectionDefinitions = this.selectionDefService.getAll();
  }

}
