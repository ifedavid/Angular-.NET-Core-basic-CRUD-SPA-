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

  constructor(private userService: UsersService, private fb: FormBuilder, private route: Router, private activatedRoute: ActivatedRoute) {
    this.createForm();
  }




  loading = false;

  //form arrays is empty?
  ArrayIsEmpty = true;

  errorMessage: string;

  //Array to store recorded expense
  CategoryData: any[] = [];

  CategoryForm: FormGroup;
  categoryData: any[] = [];

  CategoryId: string;

  SavedCategories = this.fb.group({
    SavedCategoryName: ['', Validators.required],
    SavedCategoryAmount: ['', Validators.required],

 });

createForm() {
  this.CategoryForm = this.fb.group({
    Categories: this.fb.array([], Validators.required),
  });
}


get Categories(): FormArray {
  return this.CategoryForm.get('Categories') as FormArray;
  }

 

  ngOnInit() {
    this.getCategories();
    this.CheckCategoryArray();
  }

  //Method to check if forms array is empty
  CheckCategoryArray() {
    if (this.Categories.length <= 0) {
      this.ArrayIsEmpty = true;
    } else {
      this.ArrayIsEmpty = false;
    }
  }



  getCategories() {
    this.loading = true;
    this.CategoryId = this.activatedRoute.snapshot.paramMap.get('id').toString();

    this.userService.getCategories(this.CategoryId).subscribe(
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
    this.CheckCategoryArray();
  }

  DeleteCategory(index) {
    this.Categories.removeAt(index);
    this.CheckCategoryArray();
  }



  SaveCategories() {
    this.loading = true;
    this.errorMessage = '';
    console.log(this.Categories.value);

    this.categoryData.push(this.Categories.value);

    let currentDayId = this.activatedRoute.snapshot.paramMap.get('id').toString();

    this.userService.SaveCategories(this.Categories.value, currentDayId).subscribe(
      result => {
        console.log(result);
        this.loading = false;
        this.Categories.clear();
        this.CheckCategoryArray();
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




