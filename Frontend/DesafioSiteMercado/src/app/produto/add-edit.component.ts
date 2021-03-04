import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { CurrencyMaskInputMode, NgxCurrencyModule } from "ngx-currency";

import { ProdutoService } from '../../app/services/produto.service';
import { AlertService } from '../../app/services/alert.service';
import * as _ from 'lodash';

@Component({
    selector: 'app-photo-base64',
    templateUrl: './add-edit.component.html',
    styleUrls: ['./add-edit.component.css']
})
export class AddEditComponent implements OnInit {
    formCadProduto!: FormGroup;
    id!: number;
    isAddMode!: boolean;
    loading = false;
    submitted = false;
    imageError: string;
    isImageSaved: boolean;
    cardImageBase64: string;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private produtoService: ProdutoService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        this.id = this.route.snapshot.params['id'];
        this.isAddMode = !this.id;

        this.formCadProduto = this.formBuilder.group({
            id: [null],
            nome: ['', Validators.required],
            valorVenda: ['', Validators.required],
            imagem: ['',Validators.required]
        });

        if (!this.isAddMode) {
            this.produtoService.getById(this.id)
                .pipe(first())
                .subscribe(x => this.formCadProduto.patchValue(x.items[0], this.cardImageBase64 = x.items[0]["imagem"]));
        }
    }

    // convenience getter for easy access to form fields
    get f() { return this.formCadProduto.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.formCadProduto.invalid) {
            return;
        }

        this.loading = true;
        if (this.isAddMode) {
            this.createUser();
        } else {
            this.updateUser();
        }
    }

    private createUser() {
        this.formCadProduto.value["imagem"] = this.cardImageBase64;
        this.formCadProduto.value["id"] = 0;
        this.produtoService.create(this.formCadProduto.value)
            .pipe(first())
            .subscribe(() => {
                this.alertService.success('Produto adicionado', { keepAfterRouteChange: true });
                this.router.navigate(['../'], { relativeTo: this.route });
            })
            .add(() => this.loading = false);
    }

    private updateUser() {
        this.formCadProduto.value["imagem"] = this.cardImageBase64;
        this.produtoService.update(this.formCadProduto.value)
            .pipe(first())
            .subscribe(() => {
                this.alertService.success('Produto atualizado', { keepAfterRouteChange: true });
                this.router.navigate(['../../'], { relativeTo: this.route });
            })
            .add(() => this.loading = false);
    }

    fileChangeEvent(fileInput: any) {
        this.imageError = null;
        if (fileInput.target.files && fileInput.target.files[0]) {
            // Size Filter Bytes
            const max_size = 20971520;
            const allowed_types = ['image/png', 'image/jpeg'];
            const max_height = 15200;
            const max_width = 25600;

            if (fileInput.target.files[0].size > max_size) {
                this.imageError =
                    'Maximum size allowed is ' + max_size / 1000 + 'Mb';

                return false;
            }

            if (!_.includes(allowed_types, fileInput.target.files[0].type)) {
                this.imageError = 'Only Images are allowed ( JPG | PNG )';
                return false;
            }
            const reader = new FileReader();
            reader.onload = (e: any) => {
                const image = new Image();
                image.src = e.target.result;
                image.onload = rs => {
                    const img_height = rs.currentTarget['height'];
                    const img_width = rs.currentTarget['width'];

                    if (img_height > max_height && img_width > max_width) {
                        this.imageError =
                            'Maximum dimentions allowed ' +
                            max_height +
                            '*' +
                            max_width +
                            'px';
                        return false;
                    } else {
                        const imgBase64Path = e.target.result;
                        this.formCadProduto
                        this.cardImageBase64 = imgBase64Path;
                        this.isImageSaved = true;
                        // this.previewImagePath = imgBase64Path;
                    }
                };
            };

            reader.readAsDataURL(fileInput.target.files[0]);
        }
    }

    removeImage() {
        this.cardImageBase64 = null;
        this.isImageSaved = false;
    }
}

// export function requiredFileType(type: string) {
//     return function (control: FormControl) {
//         const file = control.value;
//         if (file) {
//             const extension = file.name.split('.')[1].toLowerCase();
//             if (type.toLowerCase() !== extension.toLowerCase()) {
//                 return {
//                     requiredFileType: true
//                 };
//             }

//             return null;
//         }

//         return null;
//     };
// }