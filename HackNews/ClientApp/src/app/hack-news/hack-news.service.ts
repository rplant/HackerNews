import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, ReplaySubject } from 'rxjs';
import { HackerNewsStory } from './hack-news-story.model';


@Injectable({
  providedIn: 'root'
})
export class HackNewsService {
  public hackProject: ReplaySubject<any> = new ReplaySubject(1);

  constructor(private http: HttpClient,@Inject("BASE_URL") private baseUrl: string) {  }

  getNewHackerStories(searchTerm: string): Observable<HackerNewsStory[]> {

    this.http.get<HackerNewsStory[]>(`${this.baseUrl}api/hacknews?searchTerm=${searchTerm}`)
      .subscribe(result => this.hackProject.next(result));
    return this.hackProject;
  }
}

