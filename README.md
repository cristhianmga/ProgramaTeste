# AprendendoNovaVersao

# Passo 1
Necessário a instalação do sdk do .net 6.
# Passo 2
nuget restore AprendendoNovaVersao
# Passo 3 
verificar configuração de conexão com o banco de dados no arquivo appsettings.json
# Passo 4 
dotnet ef database update no console
# Passo 5 
executar projeto 
# Passo 6 
Utilizar a API Usuario para criar um usuário e senha para autenticação
# Passo 7 
Realizar a autenticação na API de autenticação 
# Passo 8
Utilizar o token gerado no botão Authorize no canto superior direito da pagina, no formato Bearer + TOKEN 
