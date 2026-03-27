export interface PessoaResumoDTO {
    id: string;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface RelatorioFinanceiroPessoaDTO {
    pessoas: PessoaResumoDTO[];
    totalGeralReceitas: number;
    totalGeralDespesas: number;
    saldoLiquidoGeral: number;
}

export interface CategoriaResumoDTO {
    id: string;
    descricao: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface RelatorioCategoriaDTO {
    categorias: CategoriaResumoDTO[];
    totalGeralReceitas: number;
    totalGeralDespesas: number;
    saldoLiquidoGeral: number;
}