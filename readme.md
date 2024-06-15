# Instalação

Para ter o código disponível em sua maquina utilize o comando:
`$ git clone https://github.com/BernardoSsilva/StoryTrails_backEnd.git`

Após isso crie um ambiente virtual python em dentro do projeto por meio do comando

Windows:
`$ py -m venv venv`

Linux:
`$python3 -m venv venv`

apos isso realize a instalação das dependências por meio do comando

`pip install -r requirements.txt`

# Execução do projeto

para realizar a compilação do projeto deve ser utilizado o comando

Window:
`$ py manage.py runserver`

Linux:
`$ python3 manage.py runserver`
após isso a api estara em funcionamento na porta :8000

para ter acesso ao perfil de administrador utilize o comando

`$ py manage.py createsuperuser`

para criar sua conta e acesse o link `localhost:8000/admin`
