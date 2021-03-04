import {​​​​ Router }​​​​ from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { environment } from '../../environments/environment';

const baseUrl = `${environment.apiUrlProduto}`;

@Injectable({ providedIn: 'root' })
export class ProdutoService {
    constructor(private http: HttpClient , private router: Router) { }

    getAll() {
        return this.http.get<any>(baseUrl + '/api/Produto/getList');
    }

    getById(id: number) {
        return this.http.get<any>(`${baseUrl}/api/Produto/GetById/${id}`);
    }

    create(params: any) {
        console.log(params);
        return this.http.post(baseUrl + '/api/Produto/Insert', params);
    }

    update(params: any) {
        return this.http.put(baseUrl + '/api/Produto/Update', params);
    }

    delete(id: number) {
        return this.http.delete(`${baseUrl}` +'/api/Produto/Delete/'+ `${id}`);
    }
}