import { useState } from 'react';
import { api } from '../lib/axios';

export function FormTransacao({ pessoas, categorias, aoSucesso }: any) {
  const [desc, setDesc] = useState('');
  const [valor, setValor] = useState(0);
  const [tipo, setTipo] = useState('Receita');
  const [idPessoa, setIdPessoa] = useState('');
  const [idCat, setIdCat] = useState('');

  const salvar = async (e: React.FormEvent) => {
    e.preventDefault();
    const payload = { descricao: desc, valor, tipo, idPessoa, idCategoria: idCat };
    await api.post('/Transacao', payload);
    aoSucesso();
    alert("Lançamento realizado!");
  };

  return (
    <form onSubmit={salvar} className="bg-gray-800 p-8 rounded-2xl border border-gray-700 shadow-2xl max-w-2xl mx-auto space-y-4">
      <h2 className="text-2xl font-bold mb-4 text-green-400">Cadastrar Transação</h2>    
      
      <input type="text" placeholder="Descrição" required className="w-full bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none focus:border-blue-500"
        onChange={e => setDesc(e.target.value)} />
      
      <div className="grid grid-cols-2 gap-4">
        <input type="number" placeholder="Valor"  required className="bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none focus:border-blue-500"
          onChange={e => setValor(Number(e.target.value))} />

        <select className="bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none" onChange={e => setTipo(e.target.value)}>
          <option value="Receita text-green-400">Receita (+)</option>
          <option value="Despesa text-red-400">Despesa (-)</option>
        </select>
      </div>

      <select required className="w-full bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none" onChange={e => setIdPessoa(e.target.value)}>
        <option value="">Selecione uma Pessoa</option>
        {pessoas?.map((p: any) => <option key={p.id} value={p.id}>{p.nome}</option>)}
      </select>

      <select required className="w-full bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none" onChange={e => setIdCat(e.target.value)}>
        <option value="">Selecione a Categoria</option>
        {categorias?.map((c: any) => <option key={c.id} value={c.id}>{c.descricao}</option>)}
      </select>

      <button type="submit" className="w-full bg-green-600 hover:bg-green-500 py-4 rounded-xl font-bold text-lg transition-all active:scale-95 shadow-lg">
        Cadastrar Lançamento
      </button>
    </form>
  );
}