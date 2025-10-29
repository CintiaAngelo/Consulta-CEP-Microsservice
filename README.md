# 📮 Serviço de Consulta de CEP

Um serviço completo de consulta de CEP que consome a API do **ViaCEP** e persiste os dados em banco de dados.

---

## 👥 Desenvolvedores

- **Cintia Cristina Braga Angelo** - RM552253  
- **Henrique Mosseri** - RM552240

---

## 🚀 Funcionalidades

✅ Consulta de CEP via API ViaCEP  
✅ Persistência em banco de dados MySQL  
✅ API RESTful com endpoints documentados  
✅ Validação de CEP  
✅ Prevenção de duplicatas  
✅ Tratamento de erros  

---

## 🛠️ Tecnologias Utilizadas

- **.NET 8** - Framework principal  
- **Entity Framework Core** - ORM para acesso a dados  
- **MySQL** - Banco de dados  
- **Swagger/OpenAPI** - Documentação da API  
- **HttpClient** - Consumo de API externa  

---

## 📋 Endpoints da API

### **POST /api/cep**
Consulta um CEP e salva no banco de dados.

**Request:**

```json
{
    "cep": "01310-100"
}
```

**Response:**

```json
{
    "id": 1,
    "cepCode": "01310100",
    "logradouro": "Avenida Paulista",
    "complemento": "de 1222 a 1498 - lado par",
    "bairro": "Bela Vista",
    "localidade": "São Paulo",
    "uf": "SP",
    "ibge": "3550308",
    "gia": "1004",
    "ddd": "11",
    "siafi": "7107",
    "dataConsulta": "2025-10-29T14:30:00"
}
```

### **GET /api/cep**
Lista todos os CEPs consultados e salvos no banco.

---

## 🗃️ Estrutura do Banco de Dados

```sql
CREATE TABLE Ceps (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Cep VARCHAR(8) NOT NULL,
    Logradouro VARCHAR(255),
    Complemento VARCHAR(255),
    Bairro VARCHAR(100),
    Localidade VARCHAR(100),
    Uf VARCHAR(2),
    Ibge VARCHAR(10),
    Gia VARCHAR(10),
    Ddd VARCHAR(3),
    Siafi VARCHAR(10),
    DataConsulta DATETIME NOT NULL,
    INDEX idx_cep (Cep)
);
```

---

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas:

```
CepServiceApp/
├── Controllers/          # Endpoints da API
├── Models/              # Entidades e DTOs
├── Services/            # Lógica de negócio
├── Repositories/        # Acesso a dados
├── Data/               # Contexto do banco
└── Program.cs          # Configuração da aplicação
```

---

## 📦 Como Executar

### Pré-requisitos
- .NET 8 SDK  
- MySQL Server  
- MySQL Workbench (opcional)

### Configuração

1️⃣ Clone o repositório:

```bash
git clone <url-do-repositorio>
cd CepServiceApp
```

2️⃣ Configure o banco de dados:

```sql
CREATE DATABASE CepDatabase;
```

3️⃣ Configure a connection string no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CepDatabase;Uid=root;Pwd=;"
  }
}
```

4️⃣ Execute a aplicação:

```bash
dotnet restore
dotnet run
```

5️⃣ Acesse a documentação:

```
http://localhost:5191/swagger
```

---

## 🧪 Testes

### CEPs para teste:
- 01310-100 - Avenida Paulista, São Paulo/SP  
- 20040020 - Rua Primeiro de Março, Rio de Janeiro/RJ  
- 40070-110 - Rua Chile, Salvador/BA  
- 80020030 - Rua XV de Novembro, Curitiba/PR  

### Exemplo de uso:

```bash
# Consultar CEP
curl -X POST "http://localhost:5191/api/cep"      -H "Content-Type: application/json"      -d '{"cep":"01310-100"}'

# Listar CEPs
curl "http://localhost:5191/api/cep"
```

---

## 🔒 Validações

- Formatação do CEP (8 dígitos numéricos)  
- Remoção de hífens e espaços  
- Verificação de CEP existente no banco  
- Tratamento de erros da API ViaCEP  
- Validação de campos obrigatórios  

---

## 📝 Critérios de Avaliação Atendidos

### Funcionalidade (0.6 pontos)
✅ CEP consultado no ViaCEP corretamente  
✅ Dados salvos no banco de dados  
✅ API retorna lista completa de CEPs  
✅ Tratamento de erros (CEP inválido, ViaCEP indisponível)

### Código (0.4 pontos)
✅ Estrutura em camadas (Domain, Repository, Service, Controller)  
✅ Uso correto de async/await  
✅ Injeção de dependência  
✅ Código limpo e bem organizado  

---

