import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FormArray, FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  constructor(private userService: UsersService, private fb: FormBuilder) { 
 this.createForm();
  }
  categoryData: any[] = [];
  CategoryData: string[] = [];

  CategoryForm: FormGroup;
    
  

createForm() {
  this.CategoryForm = this.fb.group({
    Categories: this.fb.array([])
  });
}

 
get Categories(): FormArray {
  return this.CategoryForm.get('Categories') as FormArray
}


  ngOnInit() { 
  }

  getCategories() {

    this.userService.getCategories().subscribe(
      result => {
        console.log(result);
      },
      error => {
        console.log(error);
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
    this.Categories.removeAt(index)
  }


  SaveCategories() {
    
 
    console.log(this.Categories.value);

    this.userService.SaveCategories(this.Categories.value).subscribe(
      result => {
        console.log(result);
      },
      error => {
        console.log(error);
      }
    )
  }

}




