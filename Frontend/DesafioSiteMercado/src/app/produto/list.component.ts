import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { ProdutoService } from '../services/produto.service';

@Component({ templateUrl: 'list.component.html' })

export class ListComponent implements OnInit {
    produtos!: any;

    constructor(private produtoService: ProdutoService) { }

    ngOnInit() {
        this.produtoService.getAll()
            .pipe(first())
            .subscribe(prod => this.produtos = prod.items);
    }

    deleteProduto(id: number) {
        const produto = this.produtos.find(x => x.id === id);
        if (!produto) return;

        this.produtoService.delete(id)
            .pipe(first())
            .subscribe(() => this.produtos = this.produtos.filter(x => x.id !== id));
    }
}