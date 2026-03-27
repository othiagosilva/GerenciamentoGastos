import { useEffect, useState, useCallback } from 'react';
import { api } from './lib/axios';
import { FormTransacao } from './components/FormTransacao';
import { FormPessoa } from './components/FormPessoa';
import { FormCategoria } from './components/FormCategoria';
import { FormExcluirPessoa } from './components/FormExcluirPessoa';
import { FormAtualizarPessoa } from './components/FormAtualizarPessoa';

interface SelectOption { id: string; nome?: string; descricao?: string; }

function App() {
  const [dadosPessoas, setDadosPessoas] = useState<any>(null);
  const [dadosCategorias, setDadosCategorias] = useState<any>(null);
  
  const [listaPessoas, setListaPessoas] = useState<SelectOption[]>([]);
  const [listaCategorias, setListaCategorias] = useState<SelectOption[]>([]);

  const [abaAtiva, setAbaAtiva] = useState<'relFinPessoas' | 'relFinCategorias' | 'cadastroTransacao' | 'cadastroPessoa' | 'cadastroCategoria' | 'atualizarPessoa' | 'excluirPessoa'>('relFinPessoas');
  const [carregando, setCarregando] = useState(false);

  const carregarTudo = useCallback(async () => {
  setCarregando(true);
  try {
    const [resRelPessoa, resRelCat, resListPessoa, resListCat] = await Promise.all([
      api.get('/RelatorioFinanceiroPessoa'),
      api.get('/RelatorioFinanceiroCategoria'),
      api.get('/Pessoa'),
      api.get('/Categoria')
    ]);

    setDadosPessoas(resRelPessoa.data);
    setDadosCategorias(resRelCat.data);
    setListaPessoas(resListPessoa.data);
    let categoriasBrutas = resListCat.data.categorias || resListCat.data;
    if (!Array.isArray(categoriasBrutas)) categoriasBrutas = [];
    const categoriasNormalizadas = categoriasBrutas.map((c: any) => ({
      id: c.id || c.idCategoria,
      descricao: c.descricao || c.Descricao
    }));

    setListaCategorias(categoriasNormalizadas);
  } catch (err) {
    console.error("Erro ao buscar dados:", err);
  } finally {
    setCarregando(false);
  }
}, []);

  useEffect(() => { carregarTudo(); }, [carregarTudo]);

  return (
    <div className="min-h-screen bg-gray-900 text-white p-8 font-sans">
      <div className="max-w-7xl mx-auto">
        <h1 className="text-3xl font-bold mb-8 text-blue-400">Gerenciamento de Gastos</h1>

        <div className="flex bg-gray-800 p-1 rounded-xl mb-8 w-fit border border-gray-700">
          <button onClick={() => setAbaAtiva('relFinPessoas')} className={`px-6 py-2 rounded-lg ${abaAtiva === 'relFinPessoas' ? 'bg-blue-600' : 'text-gray-400'}`}>Relatório Financeiro Pessoas</button>
          <button onClick={() => setAbaAtiva('relFinCategorias')} className={`px-6 py-2 rounded-lg ${abaAtiva === 'relFinCategorias' ? 'bg-blue-600' : 'text-gray-400'}`}>Relatório Financeiro Categorias</button>
          <button onClick={() => setAbaAtiva('cadastroTransacao')} className={`px-6 py-2 rounded-lg ${abaAtiva === 'cadastroTransacao' ? 'bg-green-600 text-white' : 'text-gray-400'}`}>+ Novo Lançamento</button>
          <button onClick={() => setAbaAtiva('cadastroPessoa')} className={`px-6 py-2 rounded-lg ${abaAtiva === 'cadastroPessoa' ? 'bg-purple-600 text-white' : 'text-gray-400'}`}>+ Nova Pessoa</button>
          <button onClick={() => setAbaAtiva('cadastroCategoria')} className={`px-6 py-2 rounded-lg ${abaAtiva === 'cadastroCategoria' ? 'bg-yellow-600 text-white' : 'text-gray-400'}`}>+ Nova Categoria</button>
          <button onClick={() => setAbaAtiva('excluirPessoa')} className={`px-6 py-2 rounded-lg ${abaAtiva === 'excluirPessoa' ? 'bg-red-600 text-white' : 'text-gray-400'}`}>- Excluir Pessoa</button>
          <button onClick={() => setAbaAtiva('atualizarPessoa')} className={`px-6 py-2 rounded-lg ${abaAtiva === 'atualizarPessoa' ? 'bg-blue-600 text-white' : 'text-gray-400'}`}>- Atualizar Pessoa</button>

        </div>

        {carregando && !dadosPessoas ? (
          <div className="animate-pulse text-blue-400 text-center p-10">Atualizando dados...</div>
        ) : (
          <div className="space-y-6">
            {abaAtiva === 'relFinPessoas' && dadosPessoas && (
              <>
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                  <Card título="Receitas" valor={dadosPessoas.totalGeralReceitas} cor="text-green-400" />
                  <Card título="Despesas" valor={dadosPessoas.totalGeralDespesas} cor="text-red-400" />
                  <Card título="Saldo" valor={dadosPessoas.saldoLiquidoGeral} cor="text-blue-400" />
                </div>
                <Tabela título="Saldo por Pessoa" colunas={['Nome', 'Saldo']} dados={dadosPessoas.pessoas.map((p: any) => ({ n: p.nome, v: p.saldo }))} />
              </>
            )}

            {abaAtiva === 'relFinCategorias' && dadosCategorias && (
              <>
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                  <Card título="Receitas" valor={dadosCategorias.totalGeralReceitas} cor="text-green-400" />
                  <Card título="Despesas" valor={dadosCategorias.totalGeralDespesas} cor="text-red-400" />
                  <Card título="Saldo" valor={dadosCategorias.saldoLiquidoGeral} cor="text-blue-400" />
                </div>
                <Tabela título="Saldo por Categoria" colunas={['Categoria', 'Saldo']} dados={dadosCategorias.categorias.map((c: any) => ({ n: c.descricao, v: c.saldo }))} />
              </>
            )}

            {abaAtiva === 'cadastroTransacao' && (
              <div className="max-w-2xl mx-auto">
                <FormTransacao 
                  pessoas={listaPessoas} 
                  categorias={listaCategorias} 
                  aoSucesso={() => {
                    carregarTudo();
                    setAbaAtiva('cadastroTransacao');
                  }} 
                />
              </div>
            )}

            {abaAtiva === 'cadastroPessoa' && (
              <div className="max-w-2xl mx-auto">
                <FormPessoa aoSucesso={() => {
                    carregarTudo();
                  }} 
                />
              </div>
            )}

            {abaAtiva === 'cadastroCategoria' && (
              <div className="max-w-2xl mx-auto">
                <FormCategoria aoSucesso={() => {
                    carregarTudo();
                    setAbaAtiva('cadastroTransacao');
                  }} 
                />
              </div>
            )}

            {abaAtiva === 'excluirPessoa' && (
              <div className="max-w-2xl mx-auto">
                <FormExcluirPessoa aoSucesso={() => {
                    carregarTudo();
                  }} 
                />
              </div>
            )}

            {abaAtiva === 'atualizarPessoa' && (
              <div className="max-w-2xl mx-auto">
                <FormAtualizarPessoa aoSucesso={() => {
                    carregarTudo();
                  }} 
                />
              </div>
            )}

          </div>
        )}
      </div>
    </div>
  );
}

function Card({ título, valor, cor }: any) {
  return (
    <div className="bg-gray-800 p-6 rounded-xl border border-gray-700 shadow-lg">
      <p className="text-gray-400 text-sm uppercase font-bold">{título}</p>
      <p className={`text-2xl font-mono mt-2 ${cor}`}>R$ {valor?.toLocaleString('pt-BR', { minimumFractionDigits: 2 })}</p>
    </div>
  );
}

function Tabela({ título, colunas, dados }: any) {
  return (
    <div className="bg-gray-800 rounded-xl border border-gray-700 overflow-hidden shadow-xl">
      <div className="p-4 bg-gray-750 border-b border-gray-700 font-bold text-gray-300">{título}</div>
      <table className="w-full text-left">
        <thead>
          <tr className="bg-gray-900/50 text-gray-400 text-xs uppercase">
            {colunas.map((c: any) => <th key={c} className="p-4">{c}</th>)}
          </tr>
        </thead>
        <tbody className="divide-y divide-gray-700">
          {dados.map((d: any, i: number) => (
            <tr key={i} className="hover:bg-gray-700/50 transition-colors">
              <td className="p-4 text-gray-300">{d.n}</td>
              <td className={`p-4 font-mono ${d.v >= 0 ? 'text-green-400' : 'text-red-400'}`}>R$ {d.v.toLocaleString('pt-BR', { minimumFractionDigits: 2 })}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;