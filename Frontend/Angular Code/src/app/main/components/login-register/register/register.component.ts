import { NgIf } from "@angular/common"
import { Component, OnDestroy, OnInit } from "@angular/core"
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from "@angular/forms"
import { Router, RouterModule } from "@angular/router"
import { NgToastService } from "ng-angular-popup"
import { Subscription } from "rxjs"
import { APP_CONFIG } from "src/app/main/configs/environment.config"
import { AuthService } from "src/app/main/services/auth.service"

@Component({
  selector: "app-register",
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, RouterModule],
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"],
})
export class RegisterComponent implements OnInit, OnDestroy {
  private unsubscribe: Subscription[] = [];
  
  constructor(
    private _fb: FormBuilder,
    private _service: AuthService,
    private _router: Router,
    private _toast: NgToastService,
  ) {}
  registerForm: FormGroup
  formValid: boolean
  ngOnInit(): void {
    this.createRegisterForm()
  }
  createRegisterForm() {
    this.registerForm = this._fb.group(
      {
        firstName: [null, Validators.compose([Validators.required])],
        lastName: [null, Validators.compose([Validators.required])],
        phoneNumber: [
          null,
          Validators.compose([Validators.required, Validators.minLength(10), Validators.maxLength(10)]),
        ],
        emailAddress: [null, Validators.compose([Validators.required, Validators.email])],
        password: [null, Validators.compose([Validators.required, Validators.minLength(5), Validators.maxLength(10)])],
        confirmPassword: [null, Validators.compose([Validators.required])],
      },
      { validator: [this.passwordCompareValidator] },
    )
  }
  passwordCompareValidator(fc: AbstractControl): ValidationErrors | null {
    return fc.get("password")?.value === fc.get("confirmPassword")?.value ? null : { notmatched: true }
  }
  get firstName() {
    return this.registerForm.get("firstName") as FormControl
  }
  get lastName() {
    return this.registerForm.get("lastName") as FormControl
  }
  get phoneNumber() {
    return this.registerForm.get("phoneNumber") as FormControl
  }
  get emailAddress() {
    return this.registerForm.get("emailAddress") as FormControl
  }
  get password() {
    return this.registerForm.get("password") as FormControl
  }
  get confirmPassword() {
    return this.registerForm.get("confirmPassword") as FormControl
  }
  onSubmit() {
    this.formValid = true
    if (this.registerForm.valid) {
      const register = this.registerForm.value
      register.userType = "user"
      
      const registerSubscribe = this._service.registerUser(register).subscribe((data: any) => {
        if (data.result == 1) {
          //this.toastr.success(data.data);
          this._toast.success({ detail: "SUCCESS", summary: data.data, duration: APP_CONFIG.toastDuration })
          setTimeout(() => {
            this._router.navigate(["admin"])
          }, 1000)
        } else {
          //this.toastr.error(data.message);
          this._toast.error({ detail: "ERROR", summary: data.message, duration: APP_CONFIG.toastDuration })
        }
      })
      this.formValid = false
      this.unsubscribe.push(registerSubscribe)
    }
  }
  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe())
  }
}
