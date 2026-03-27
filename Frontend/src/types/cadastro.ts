export interface CriarPessoaDTO {
    nome: string;
}

export interface CriarCategoriaDTO {
    descricao: string;
}

export interface CriarTransacaoDTO {
    descricao: string;
    valor: number;
    tipo: "Receita" | "Despesa";
    idPessoa: string;
    idCategoria: string;
}