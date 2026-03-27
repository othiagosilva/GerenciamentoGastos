import { useState } from 'react';
import { api } from '../lib/axios';

export function FormExcluirPessoa({aoSucesso }: any) {
  const [idPessoa, setIdPessoa] = useState<string | null>(null);
  const [mensagemErro, setMensagemErro] = useState('');
  const [mensagemSucesso, setMensagemSucesso] = useState('');

  const salvar = async (e: React.FormEvent) => {
    e.preventDefault();
    setMensagemErro('');
    setMensagemSucesso('');

    try {
      const resposta = await api.delete(`/Pessoa/${idPessoa}`);
      const id = resposta.data.id || resposta.data.idPessoa || null;
      setIdPessoa(id);
      setMensagemSucesso("Pessoa excluída com sucesso!");
      aoSucesso();
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
      <h2 className="text-2xl font-bold mb-4 text-red-600">Excluir Pessoa</h2>    
      
      {mensagemErro && (
      <div className="col-span-full bg-red-900/30 border border-red-500 text-red-400 p-3 rounded-lg text-sm">
         {mensagemErro}
      </div>
    )}

      {mensagemSucesso && (
      <div className="col-span-full bg-green-900/30 border border-green-500 text-green-400 p-3 rounded-lg text-sm">
        {mensagemSucesso}
      </div>
    )}

      <input type="text" placeholder="ID Pessoa" required className="w-full bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none focus:border-blue-500"
        onChange={e => setIdPessoa(e.target.value)} />

      <button type="submit" className="w-full bg-red-600 hover:bg-red-500 py-4 rounded-xl font-bold text-lg transition-all active:scale-95 shadow-lg">
        Excluir Pessoa
      </button>
    </form>
  );
}