import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FormArray, FormGroup, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';



@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  constructor(private userService: UsersService, private fb: FormBuilder, private route: Router) {
 this.createForm();

  }




  loading = false;

  errorMessage: string;

  CategoryData: any[] = [];

  CategoryForm: FormGroup;
  categoryData: any;



 SavedCategories = this.fb.group({
   SavedCategoryName: [''],
   SavedCategoryAmount: [''],

 });

createForm() {
  this.CategoryForm = this.fb.group({
    Categories: this.fb.array([]),
  });
}


get Categories(): FormArray {
  return this.CategoryForm.get('Categories') as FormArray;
}




  ngOnInit() {
    this.getCategories();
  }

  getCategories() {
    this.loading = true;
    let currentDate: string = localStorage.getItem('TodayDate');
    this.userService.getCategories(currentDate).subscribe(
      result => {

        this.categoryData = result.value;

        this.loading = false;

      },
      error => {
        console.log(error);
        this.loading = false;
      }
    );
  }

  AddCategory() {
    this.Categories.push(this.fb.group(
      {
        CategoryName: [''],
        CategoryAmount: ['']
      }
    ));
  }

  DeleteCategory(index) {
    this.Categories.removeAt(index);
  }


  SaveCategories() {
    this.loading = true;
    this.errorMessage = '';
    console.log(this.Categories.value);

    this.categoryData.push(this.Categories.value);

    let currentDay = localStorage.getItem('TodayDate');

    this.userService.SaveCategories(this.Categories.value, currentDay).subscribe(
      result => {
        console.log(result);
        this.loading = false;
        this.getCategories();
      },
      error => {
        console.log(error);
        this.loading = false;
        this.errorMessage = error.error.message;
        this.getCategories();
      }
    );
  }


  UpdateSavedCategory(i) {
    this.loading = true;
    this.CategoryData = [];
    this.CategoryData.push({
     Id: this.categoryData[i].id,
     CategoryName: this.SavedCategories.value.SavedCategoryName,
     CategoryAmount: this.SavedCategories.value.SavedCategoryAmount
    });



    this.userService.UpdateCategories(this.CategoryData[0]).subscribe(
      result => {
        console.log(result);
        this.getCategories();
        this.loading = false;
      },
      error => {
        console.log(error);
        this.loading = false;
      }

    );
  }

  DeleteSavedCategory(i) {
    this.loading = true;
    this.CategoryData = [];
    this.CategoryData.push({
      id: this.categoryData[i].id
    });
    this.userService.DeleteCategories(this.CategoryData[0]).subscribe(
      result => {
        console.log(result);
        this.getCategories();
        this.loading = false;
      },
      error => {
        console.log(error);
        this.loading = false;
      }
    );
  }

}




