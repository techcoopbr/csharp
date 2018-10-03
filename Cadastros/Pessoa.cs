using System;

namespace Modelo
{

    public class Pessoa
    {
        //dados básicos
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }

        //dados de endereço
        public string Endereco { get; set; }
        public string Numerorua { get; set; }
        public string Bairro { get; set; }
        public int Cxpostal { get; set; }
        public string Complemento { get; set; }

        //herdar cidade
        public int Cidade { get; set; }
        public string Cep { get; set; }

        //dados de fone
        public string Fone01 { get; set; }
        public string Fone02 { get; set; }
        public string Fone03 { get; set; }
        public string Fone04 { get; set; }

        //dados de contato
        public string Contato { get; set; }
        public string Celular { get; set; }

        public char Pessoafisicajuridica { get; set; }
        public string Cpfcnpj { get; set; }
        public string Rgie { get; set; }
        public string Orgaoemissorrg { get; set; }
        public DateTime Dataemissaorg { get; set; }

        public string Contato { get; set; }
        public string Celular { get; set; }
        public DateTime Datanascimento { get; set; }

        public string Email { get; set; }
        public string Homepage { get; set; }
        public string Msn { get; set; } //skype, facebook, etc

        //filiação e esposa
        public string Nomepai { get; set; }
        public string Nomemae { get; set; }
        public string Conjuge { get; set; }

        public char Estadocivil { get; set; }
        public char Sexo { get; set; }

        public string Referenciacomercial { get; set; }
        public string Restricaomotivo { get; set; }
        public byte Bloquearvenda { get; set; }

        public string Inscricaomunicipal { get; set; }
        public byte Contribuinteicms { get; set; }
        public byte Contribuinteipi { get; set; }
        public int Suframa { get; set; }

        public DateTime Datahora_atualizacao { get; set; }
        public DateTime Dataultimacompra { get; set; }

        public byte Cliente { get; set; }
        public byte Funcionario { get; set; }
        public byte Fornecedor { get; set; }
        public byte Motorista { get; set; }
        public byte transportador { get; set; }
        public byte Consultor { get; set; }
        public byte Entregapecas { get; set; }
        public byte Representante { get; set; }
        public byte Vendedor { get; set; }
        public byte Operadoracartao { get; set; }
        public byte Consumidorfinal { get; set; }
        public byte Ativo { get; set; }

        public byte Tributacao { get; set; }
        public string Cte_rntrc { get; set; }

        public string Trab_cnpj { get; set; }
        public string Trab_ie { get; set; }
        public string Trab_razaosocial { get; set; }
        public string Trab_fantasia { get; set; }
        public string Trab_endereco { get; set; }
        public string Trab_numero { get; set; }
        public string Trab_bairro { get; set; }
        public string Trab_cidadeuf { get; set; }

        public byte Classificacao { get; set; }
        public byte Atacado { get; set; }

        public string iesustituto { get; set; }


        public decimal Comissao { get; set; }
        public decimal Comissaoavista { get; set; }
        public decimal Comissaoaprazo { get; set; }

        public string Ididentifid { get; set; }

        public decimal Limitecredito { get; set; }

        public byte Bloqueialimite { get; set; }
        public byte Bloqueiadebito { get; set; }
        public byte Msglimite { get; set; }
        public byte Msgdebito { get; set; }
        public byte Operacaopadrao { get; set; }

        public string origemrg { get; set; }
        public byte Opservico { get; set; }
        public byte Totalhorasmensais { get; set; }

        public DateTime Hora1 { get; set; }
        public DateTime Hora2 { get; set; }
        public DateTime Hora3 { get; set; }
        public DateTime Hora4 { get; set; }
        public DateTime Hora5 { get; set; }
        public DateTime Hora6 { get; set; }
        public DateTime Hora7 { get; set; }
        public DateTime Hora8 { get; set; }
        public DateTime Hora9 { get; set; }
        public DateTime Hora10 { get; set; }
        public DateTime Hora11 { get; set; }
        public DateTime Hora12 { get; set; }
        public DateTime Hora13 { get; set; }
        public DateTime Hora14 { get; set; }
        public DateTime Hora15 { get; set; }
        public DateTime Hora16 { get; set; }
        public DateTime Hora17 { get; set; }
        public DateTime Hora18 { get; set; }
        public DateTime Hora19 { get; set; }
        public DateTime Hora20 { get; set; }
        public DateTime Hora21 { get; set; }
        public DateTime Hora22 { get; set; }
        public DateTime Hora23 { get; set; }
        public DateTime Hora24 { get; set; }
        public DateTime Hora25 { get; set; }
        public DateTime Hora26 { get; set; }
        public DateTime Hora27 { get; set; }
        public DateTime Hora28 { get; set; }

        public decimal Parcelamaxima { get; set; }
        public decimal Desc_avista { get; set; }
        public decimal Desc_aprazo { get; set; }
        public decimal Desc_adebito { get; set; }
        public decimal Desc_acredito { get; set; }

        public decimal Desc_acc { get; set; }

        public byte Usabarras { get; set; }
        public byte Regimetributario { get; set; }
        public byte Imprimenota { get; set; }
        public byte Bloqueiadebitosenha { get; set; }
        public byte Bloqueialimitesenha { get; set; }
        public byte Diasaposvencido { get; set; }

        public decimal Valorservico { get; set; }

        public byte Idvendedor { get; set; }
        public byte pagacomissao { get; set; }
        public byte Comissaototalnavenda { get; set; }

        public DateTime Ultimacompra { get; set; }

        public decimal Valorultimacompra { get; set; }

        public byte Documento { get; set; }
        public byte Mostraimpostoibpt { get; set; }

        public decimal Salario { get; set; }

        public string Conj_localtrab { get; set; }
        public string Conj_profissao { get; set; }
        public decimal Conj_salario { get; set; }
        public string Conj_cpf { get; set; }
        public string Conj_rg { get; set; }
        public DateTime Conj_nascimento { get; set; }
        public string Conj_tempotrabalho { get; set; }

        public string Profissao { get; set; }
        public string Tempotrabalho { get; set; }

        public byte Sincronizado { get; set; }

    }
}
