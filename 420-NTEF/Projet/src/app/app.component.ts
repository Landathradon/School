import {Component, OnInit, ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit {
  title = 'Jeux des dés';
  Dices = new Array<Die>();
  points = 0;
  bonus = 0;
  totalPoints = 0;
  totalPointsCumulated = 0;

  replay(): void{
    this.points = 0;
    this.bonus = 0;
    this.totalPoints = 0;
    this.Dices = this.generateDices();
    this.calculateDiePoints();
  }

  generateDices(): Array<Die> {
    const Dices = [];
    for (let index = 0; index < 3; index++) {
      const random = Math.floor(Math.random() * (7 - 1) + 1);
      const die = new Die('de' + random, random);
      Dices.push(die);
    }
    return Dices;
  }

  calculateDiePoints(): void{
    const dices = this.Dices;
    for (const die of dices){
      this.points += die.points;
    }

    if (dices.every((val, i, arr) => val.points === arr[0].points)){
      this.bonus += 10;
    } else if (dices[0].getPoints() === dices[1].getPoints() || dices[0].getPoints() === dices[2].getPoints()
      || dices[1].getPoints() === dices[2].getPoints()){
      this.bonus += 5;
    }
    this.totalPoints = this.points + this.bonus;
    this.totalPointsCumulated += this.totalPoints;
  }

  ngOnInit(): void {
    this.replay();
  }
}

class Die {
  image: string;
  points: number;

  constructor(image: string, points: number) {
    this.image = image;
    this.points = points;
  }

  getImageSrc(): string {
    return './assets/images/' + this.image + '.png';
  }

  getPoints(): number {
    return this.points;
  }
}
