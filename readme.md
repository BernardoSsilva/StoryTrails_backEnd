# 📚 StoryTrails API (Python Version)

## 📌 Sobre o Projeto

A **StoryTrails API** é uma aplicação backend desenvolvida com **Django** como parte de um trabalho acadêmico de Engenharia de Software.

O sistema consiste em uma **API REST para gerenciamento de uma coleção de livros**, permitindo operações completas de cadastro, autenticação e administração de dados.

---

## 🎯 Objetivo

O projeto foi desenvolvido com o objetivo de:

* Praticar construção de APIs REST com Django
* Implementar autenticação segura com JWT
* Trabalhar com gerenciamento de dados estruturados
* Consolidar conceitos de backend em um contexto acadêmico

---

## 🔐 Autenticação

A API utiliza **JWT (JSON Web Token)** para autenticação, permitindo:

* Login seguro de usuários
* Proteção de rotas privadas
* Controle de acesso aos recursos da aplicação

---

## ⚙️ Tecnologias Utilizadas

* Python
* Django
* Django REST Framework
* JWT Authentication
* SQLite (ou outro banco configurado)

---

## 🚀 Funcionalidades

* 📚 Cadastro de livros
* ✏️ Atualização de informações
* ❌ Remoção de livros
* 📄 Listagem de livros
* 🔐 Autenticação de usuários via JWT
* 🛠️ Painel administrativo do Django

---

## 🧩 Estrutura do Projeto

O projeto segue a estrutura padrão do Django, com organização voltada para APIs REST:

* **apps/** → Módulos da aplicação
* **models/** → Estrutura dos dados
* **views/** → Lógica dos endpoints
* **serializers/** → Transformação de dados
* **urls/** → Rotas da API

---

## 🛠️ Instalação e Execução

### 🔹 1. Clonar o repositório

```bash id="cmd_clone"
git clone https://github.com/BernardoSsilva/StoryTrails_backEnd.git
cd StoryTrails_backEnd
```

---

### 🔹 2. Criar ambiente virtual

#### Windows

```bash id="cmd_win_venv"
py -m venv venv
```

#### Linux

```bash id="cmd_linux_venv"
python3 -m venv venv
```

---

### 🔹 3. Ativar ambiente virtual

#### Windows

```bash id="cmd_win_activate"
venv\Scripts\activate.bat
```

#### Linux

```bash id="cmd_linux_activate"
source venv/bin/activate
```

---

### 🔹 4. Instalar dependências

```bash id="cmd_install"
pip install -r requirements.txt
```

---

### 🔹 5. Executar o projeto

#### Windows

```bash id="cmd_run_win"
py manage.py runserver
```

#### Linux

```bash id="cmd_run_linux"
python3 manage.py runserver
```

📌 A API estará disponível em:
http://localhost:8000

---

### 🔹 6. Criar superusuário

```bash id="cmd_superuser"
py manage.py createsuperuser
```

📌 Acesse o painel administrativo:
http://localhost:8000/admin

---

## 🧪 Contexto de Desenvolvimento

Este projeto foi desenvolvido como parte de um trabalho acadêmico, com foco em:

* Aprendizado de backend com Django
* Implementação de autenticação
* Estruturação de APIs REST
* Manipulação de dados em banco relacional

---

## 📈 Possíveis Melhorias

* Implementação de testes automatizados
* Paginação e filtros avançados
* Documentação da API (Swagger)
* Melhor organização em camadas
* Deploy em ambiente cloud

---

## 💬 Considerações Finais

Este projeto representa uma base sólida em:

* Desenvolvimento de APIs com Django
* Autenticação via JWT
* Estruturação de aplicações backend
* Aplicação prática de conceitos acadêmicos

