import { useState } from 'react';
import axios from 'axios';

function FormTransacao({ pessoas, categorias, aoSucesso }: any) {
  const [desc, setDesc] = useState('');
  const [valor, setValor] = useState(0);
  const [tipo, setTipo] = useState('Receita');
  const [idPessoa, setIdPessoa] = useState('');
  const [idCat, setIdCat] = useState('');

  const salvar = async (e: React.FormEvent) => {
    e.preventDefault();
    const payload = { descricao: desc, valor, tipo, idPessoa, idCategoria: idCat };
    await axios.post('https://localhost:7143/api/Transacao', payload);
    aoSucesso();
    alert("Lançamento realizado!");
  };

  return (
    <form onSubmit={salvar} className="bg-gray-800 p-6 rounded-xl border border-gray-700 shadow-2xl grid grid-cols-1 md:grid-cols-2 gap-4">
      <h2 className="text-xl font-bold col-span-full text-blue-400">Novo Lançamento</h2>
      
      <input type="text" placeholder="Descrição" className="bg-gray-900 p-3 rounded border border-gray-700 focus:border-blue-500 outline-none" 
        onChange={e => setDesc(e.target.value)} />
      
      <input type="number" placeholder="Valor" className="bg-gray-900 p-3 rounded border border-gray-700 focus:border-blue-500 outline-none"
        onChange={e => setValor(Number(e.target.value))} />

      <select className="bg-gray-900 p-3 rounded border border-gray-700" onChange={e => setTipo(e.target.value)}>
        <option value="Receita text-green-400">Receita (+)</option>
        <option value="Despesa text-red-400">Despesa (-)</option>
      </select>

      <select className="bg-gray-900 p-3 rounded border border-gray-700" onChange={e => setIdPessoa(e.target.value)}>
        <option value="">Quem está pagando/recebendo?</option>
        {pessoas?.map((p: any) => <option key={p.id} value={p.id}>{p.nome}</option>)}
      </select>

      <select className="bg-gray-900 p-3 rounded border border-gray-700" onChange={e => setIdCat(e.target.value)}>
        <option value="">Qual a categoria?</option>
        {categorias?.map((c: any) => <option key={c.id} value={c.id}>{c.descricao}</option>)}
      </select>

      <button type="submit" className="col-span-full bg-blue-600 hover:bg-blue-700 py-3 rounded-lg font-bold transition-all shadow-lg mt-2">
        Confirmar Lançamento
      </button>
    </form>
  );
}