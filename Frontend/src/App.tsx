import { useEffect, useState, useCallback } from 'react';
import axios from 'axios';

// Interfaces para tipagem
interface ItemRelatorio { nome: string; valor: number; }
interface SelectOption { id: string; nome?: string; descricao?: string; }

function App() {
  const [dadosPessoas, setDadosPessoas] = useState<any>(null);
  const [dadosCategorias, setDadosCategorias] = useState<any>(null);
  
  const [listaPessoas, setListaPessoas] = useState<SelectOption[]>([]);
  const [listaCategorias, setListaCategorias] = useState<SelectOption[]>([]);

  const [abaAtiva, setAbaAtiva] = useState<'pessoas' | 'categorias' | 'cadastro'>('pessoas');
  const [carregando, setCarregando] = useState(false);

  const [transacao, setTransacao] = useState({
    descricao: '',
    valor: 0,
    tipo: 'Receita',
    idPessoa: '', 
    idCategoria: '' 
  });

  const carregarTudo = useCallback(async () => {
  setCarregando(true);
  try {
    const [resRelPessoa, resRelCat, resListPessoa, resListCat] = await Promise.all([
      axios.get('https://localhost:7143/api/RelatorioFinanceiroPessoa'),
      axios.get('https://localhost:7143/api/RelatorioFinanceiroCategoria'),
      axios.get('https://localhost:7143/api/Pessoa'),
      axios.get('https://localhost:7143/api/Categoria')
    ]);

    setDadosPessoas(resRelPessoa.data);
    setDadosCategorias(resRelCat.data);
    setListaPessoas(resListPessoa.data);
    console.log("Conteúdo bruto das Categorias:", resListCat.data);

    let categoriasBrutas = resListCat.data.categorias || resListCat.data;
    if (!Array.isArray(categoriasBrutas)) categoriasBrutas = [];

    // MAPEADOR: Aqui garantimos que, não importa o nome no C#, no React será 'id'
    const categoriasNormalizadas = categoriasBrutas.map((c: any) => ({
      id: c.id || c.idCategoria || c.Id || c.IdCategoria, // Tenta todos os nomes possíveis
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

  const salvarTransacao = async (e: React.FormEvent) => {
    e.preventDefault();
    
    // Log para você conferir no F12 antes de enviar
    console.log("Payload sendo enviado:", transacao);

    if (transacao.idPessoa.length < 10) return alert("Selecione uma Pessoa válida.");
    if (transacao.idCategoria.length < 10) return alert("Selecione uma Categoria válida.");

    try {
      await axios.post('https://localhost:7143/api/Transacao', transacao);
      alert("Lançamento realizado!");
      setTransacao({ descricao: '', valor: 0, tipo: 'Receita', idPessoa: '', idCategoria: '' });
      await carregarTudo();
      setAbaAtiva('pessoas');
    } catch (err: any) {
      console.error("Erro na API:", err.response?.data);
      alert("Erro na API: " + JSON.stringify(err.response?.data?.errors || err.response?.data));
    }
  };

  return (
    <div className="min-h-screen bg-gray-900 text-white p-8 font-sans">
      <div className="max-w-5xl mx-auto">
        <h1 className="text-3xl font-bold mb-8 text-blue-400">FreeBalance</h1>

        <div className="flex bg-gray-800 p-1 rounded-xl mb-8 w-fit border border-gray-700">
          <button onClick={() => setAbaAtiva('pessoas')} className={`px-6 py-2 rounded-lg ${abaAtiva === 'pessoas' ? 'bg-blue-600' : 'text-gray-400'}`}>Pessoas</button>
          <button onClick={() => setAbaAtiva('categorias')} className={`px-6 py-2 rounded-lg ${abaAtiva === 'categorias' ? 'bg-blue-600' : 'text-gray-400'}`}>Categorias</button>
          <button onClick={() => setAbaAtiva('cadastro')} className={`px-6 py-2 rounded-lg ${abaAtiva === 'cadastro' ? 'bg-green-600 text-white' : 'text-gray-400'}`}>+ Novo Lançamento</button>
        </div>

        {carregando && !dadosPessoas ? (
          <div className="animate-pulse text-blue-400 text-center p-10">Atualizando dados...</div>
        ) : (
          <div className="space-y-6">
            {abaAtiva === 'pessoas' && dadosPessoas && (
              <>
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                  <Card título="Receitas" valor={dadosPessoas.totalGeralReceitas} cor="text-green-400" />
                  <Card título="Despesas" valor={dadosPessoas.totalGeralDespesas} cor="text-red-400" />
                  <Card título="Saldo" valor={dadosPessoas.saldoLiquidoGeral} cor="text-blue-400" />
                </div>
                <Tabela título="Saldo por Pessoa" colunas={['Nome', 'Saldo']} dados={dadosPessoas.pessoas.map((p: any) => ({ n: p.nome, v: p.saldo }))} />
              </>
            )}

            {abaAtiva === 'categorias' && dadosCategorias && (
              <>
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                  <Card título="Receitas" valor={dadosCategorias.totalGeralReceitas} cor="text-green-400" />
                  <Card título="Despesas" valor={dadosCategorias.totalGeralDespesas} cor="text-red-400" />
                  <Card título="Saldo" valor={dadosCategorias.saldoLiquidoGeral} cor="text-blue-400" />
                </div>
                <Tabela título="Saldo por Categoria" colunas={['Categoria', 'Saldo']} dados={dadosCategorias.categorias.map((c: any) => ({ n: c.descricao, v: c.saldo }))} />
              </>
            )}

            {abaAtiva === 'cadastro' && (
              <form onSubmit={salvarTransacao} className="bg-gray-800 p-8 rounded-2xl border border-gray-700 shadow-2xl max-w-2xl mx-auto space-y-4">
                <h2 className="text-2xl font-bold mb-4 text-green-400">Cadastrar Transação</h2>
                
                <input type="text" placeholder="Descrição" required className="w-full bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none focus:border-blue-500"
                  value={transacao.descricao} onChange={e => setTransacao({...transacao, descricao: e.target.value})} />

                <div className="grid grid-cols-2 gap-4">
                  <input type="number" step="0.01" placeholder="Valor" required className="bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none focus:border-blue-500"
                    value={transacao.valor || ''} onChange={e => setTransacao({...transacao, valor: Number(e.target.value)})} />
                  
                  <select className="bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none"
                    value={transacao.tipo} onChange={e => setTransacao({...transacao, tipo: e.target.value})}>
                    <option value="Receita">Receita (+)</option>
                    <option value="Despesa">Despesa (-)</option>
                  </select>
                </div>

                {/* SELECT PESSOA */}
                <select required className="w-full bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none"
                  value={transacao.idPessoa} onChange={e => setTransacao({...transacao, idPessoa: e.target.value})}>
                  <option value="">Selecione a Pessoa</option>
                  {listaPessoas.map(p => <option key={p.id} value={p.id}>{p.nome}</option>)}
                </select>

                {/* SELECT CATEGORIA - CORRIGIDO */}
                <select required className="w-full bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none"
                  value={transacao.idCategoria} onChange={e => setTransacao({...transacao, idCategoria: e.target.value})}>
                  <option value="">Selecione a Categoria</option>
                  {listaCategorias.map(c => (
                    <option key={c.id} value={c.id}>
                      {c.descricao}
                    </option>
                  ))}
                </select>

                <button type="submit" className="w-full bg-green-600 hover:bg-green-500 py-4 rounded-xl font-bold text-lg transition-all active:scale-95 shadow-lg">
                  Confirmar Lançamento
                </button>
              </form>
            )}
          </div>
        )}
      </div>
    </div>
  );
}

// --- COMPONENTES INTERNOS ---
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