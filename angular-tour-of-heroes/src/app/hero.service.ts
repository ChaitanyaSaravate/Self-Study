import { Injectable } from '@angular/core';
import { Hero } from "./hero";
import { Observable, of } from "rxjs";
import { MessageService } from "./message.service";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class HeroService {
  private heroesUrl = 'api/heroes';  // URL to web api
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private messageService: MessageService, private http: HttpClient) { }

  getHeroes(): Observable<Hero[]> {
    this.log("Fetching list of Heroes");
    return this.http.get<Hero[]>(this.heroesUrl).pipe(tap(_ => this.log(`fetched heroes`)), catchError(this.handleError(`getHorses`, [])));
  }

  getHero(id: number): Observable<Hero> {
    const url = `${this.heroesUrl}/${id}`;  
    return this.http.get<Hero>(url).pipe(tap(_ => this.log(`Fetched hero id = ${id}`), catchError(this.handleError(`getHero id=${id}`))));
  }

  updateHero(hero: Hero): Observable<any>{
    return this.http.put<Hero>(this.heroesUrl, hero, this.httpOptions).pipe(tap(_ => this.log(`Updating hero id = ${hero.id}`), catchError(this.handleError(`updateHero id=${hero.id}`))));
  }

  addHero(hero: Hero):Observable<any>{
    return this.http.post<Hero>(this.heroesUrl, hero, this.httpOptions).pipe(tap((newHero: Hero) => this.log(`Adding new hero with id = ${newHero.id}`)), catchError(this.handleError(`addHero`)));
  }

  deleteHero(hero: Hero | number): Observable<any>{
    const id = typeof hero === 'number' ? hero : hero.id;
    const url = `${this.heroesUrl}/${id}`;
    return this.http.delete<Hero>(url, this.httpOptions).pipe(tap(_ => this.log(`Deleted Hero with ${id}`)), catchError(this.handleError<Hero>(`deleteHero id=${id}`)));
  }

  searchHeroes(term: string):Observable<Hero[]>{
    if(!term.trim())
    {
      return of([]);
    }
    return this.http.get<Hero[]>(`${this.heroesUrl}/?name=${term}`, this.httpOptions).pipe(tap(_ => this.log(`Searching heroes with name = ${term}`)), catchError(this.handleError('searchHeroes', [])));
  }

  private handleError<T> (operation = 'operation', result?: T)
  {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log('${operation} failed: ${error.message}');
      return of(result as T);
    }
  }

  /** Log a HeroService message with the MessageService */
private log(message: string) {
  this.messageService.add(`HeroService: ${message}`);
}
}
