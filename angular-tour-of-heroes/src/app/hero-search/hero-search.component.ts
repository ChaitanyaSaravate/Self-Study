import { Component, OnInit } from '@angular/core';
import { HeroService } from '../hero.service'
import { Hero } from '../hero';
import { Observable, of, Subject } from 'rxjs';
import {
  debounceTime, distinctUntilChanged, switchMap
} from 'rxjs/operators';

@Component({
  selector: 'app-hero-search',
  templateUrl: './hero-search.component.html',
  styleUrls: ['./hero-search.component.css']
})
export class HeroSearchComponent implements OnInit {

  heroes$ : Observable<Hero[]>;
  private searchTerms = new Subject<string>();

  constructor(private heroService:HeroService) { }

  ngOnInit() {
    this.heroes$ = this.searchTerms.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((term: string) => this.heroService.searchHeroes(term))
    )
  }

  search(name: string): void
  {
     this.searchTerms.next(name);
  }

}
