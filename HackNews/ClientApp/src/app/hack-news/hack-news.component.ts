import { Component, Inject ,OnInit } from '@angular/core';
import { HackNewsService } from './hack-news.service';
import { HackerNewsStory } from './hack-news-story.model';


@Component({
  selector: 'hack-news',
  templateUrl: './hack-news.component.html',
  styleUrls: ['./hack-news.component.css']
})
export class HackNewsComponent implements OnInit {
  public hackerNewsStories: HackerNewsStory[];
  public hackService: HackNewsService;
  pageOfItems: Array<any>;

  constructor(service: HackNewsService) {
    this.hackService = service;
  }

  ngOnInit(): void {
    this.hackService.hackProject.subscribe(active => this.hackerNewsStories = active);
    this.hackService.getNewHackerStories("");
  }

  search(event: KeyboardEvent) {
    this.hackService.getNewHackerStories((event.target as HTMLTextAreaElement).value);
  }

  open(url: string) {
    window.open(url, "_blank");
  }

  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }

}

