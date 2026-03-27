import { useState } from 'react';
import { api } from '../lib/axios';

export function FormPessoa({aoSucesso }: any) {
  const [nome, setNome] = useState('');
  const [idade, setIdade] = useState(0);
  const [mensagemErro, setMensagemErro] = useState('');
  const [mensagemSucesso, setMensagemSucesso] = useState('');

  const salvar = async (e: React.FormEvent) => {
    e.preventDefault();
    setMensagemErro('');
    setMensagemSucesso('');

    try {
      const payload = { nome, idade };
      const resposta = await api.post('/Pessoa', payload);
      const id = resposta.data.id || resposta.data.idPessoa || null;
      setMensagemSucesso("Pessoa cadastrada com sucesso!" + (id ? ` ID: ${id}` : ''));
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
      <h2 className="text-2xl font-bold mb-4 text-purple-600">Cadastrar Pessoa</h2>    
      
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

      <input type="text" placeholder="Nome" required className="w-full bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none focus:border-blue-500"
        onChange={e => setNome(e.target.value)} />
      
      <input type="number" placeholder="Idade"  required className="bg-gray-900 p-3 rounded-lg border border-gray-700 outline-none focus:border-blue-500"
        onChange={e => setIdade(Number(e.target.value))} />

      <button type="submit" className="w-full bg-purple-600 hover:bg-purple-500 py-4 rounded-xl font-bold text-lg transition-all active:scale-95 shadow-lg">
        Cadastrar Pessoa
      </button>
    </form>
  );
}