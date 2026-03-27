import { useState } from 'react';
import { api } from '../lib/axios';

export function FormCategoria({aoSucesso }: any) {
    const [descricao, setDescricao] = useState('');
    const [finalidade, setFinalidade] = useState('');
    const [mensagemErro, setMensagemErro] = useState('');

  const salvar = async (e: React.FormEvent) => {
    e.preventDefault();
    setMensagemErro('');

    try {
        const payload = { descricao, finalidade };
        await api.post('/Categoria', payload);
        aoSucesso();
        alert("Categoria cadastrada!");
    } catch (err: any) {
        if (err.response && err.response.data) {
        const data = err.response.data;

        if (typeof data === 'string') {
          setMensagemErro(data);
        } 
        else if (data.errors) {
          const mensagens = Object.values(data.errors).flat().join(", ");
          setMensagemErro(mensagens);
        }
        else if (data.message) {
          setMensagemErro(data.message);
        }
        else {
          setMensagemErro(JSON.stringify(data));
        }
      } else {
        setMensagemErro("Não foi possível conectar ao servidor.");
      }
    }
  };

  return (
    <form onSubmit={salvar} className="bg-gray-800 p-8 rounded-2xl border border-gray-700 shadow-2xl max-w-2xl mx-auto space-y-4">
      <h2 className="text-2xl font-bold mb-4 text-yellow-600">Cadastrar Categoria</h2>    
      
      {mensagemErro && (
      <div className="col-span-full bg-red-900/30 border border-red-500 text-red-400 p-3 rounded-lg text-sm">
         {mensagemErro}
      </div>
    )}

      <input type="text" placeholder="Descrição" required className="w-full bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none focus:border-blue-500"
        onChange={e => setDescricao(e.target.value)} />
      
      <input type="text" placeholder="Finalidade"  required className="bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none focus:border-blue-500"
        onChange={e => setFinalidade(e.target.value)} />

      <button type="submit" className="w-full bg-yellow-600 hover:bg-yellow-600 py-4 rounded-xl font-bold text-lg transition-all active:scale-95 shadow-lg">
        Cadastrar Categoria
      </button>
    </form>
  );
}