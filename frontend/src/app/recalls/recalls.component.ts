import { Component, OnInit } from '@angular/core';
import Recall from 'src/shared/models/Recall';
import { RecallService } from 'src/shared/services/recall.service';

@Component({
  selector: 'app-recalls',
  templateUrl: './recalls.component.html',
  styleUrls: ['./recalls.component.scss'],
})
export class RecallsComponent implements OnInit {
  recalledProducts: Recall[];

  constructor(private recallService: RecallService) {}

  ngOnInit(): void {
    this.getAllRecalls();
  }

  getAllRecalls(): void {
    this.recallService.getAllRecalls().subscribe({
      next: (res) => {
        this.recalledProducts = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
