# Sistema de Narrativa Procedural

Este repositório contém o protótipo desenvolvido para o **Trabalho de Conclusão de Curso (TCC)**.  
O projeto integra Unity, uma API REST em Python e um modelo de linguagem de larga escala (LLM) para gerar conteúdo narrativo procedural, eventos dinâmicos e diálogos adaptativos durante o gameplay.

---

## 🚀 Visão Geral do Sistema

O projeto é estruturado em três principais componentes:

### **1. Cliente Unity (C#)**
Responsável por:
- Interface com o usuário e interação do jogador
- Exibição de texto narrativo, escolhas e mensagens
- Geração procedural de masmorras
- Mecânicas de combate, exploração e inventário
- Comunicação HTTP com a API backend

### **2. API REST em Python**
Responsável por:
- Receber o contexto atual do jogo e ações do jogador
- Montar prompts dinamicamente com base no estado do jogo
- Enviar requisições estruturadas ao LLM
- Retornar respostas JSON padronizadas e validadas para a Unity

### **3. Modelo de Linguagem (LLM)**
Utilizado para:
- Gerar narrativas e opções de diálogo
- Produzir descrições contextuais, eventos, itens e entidades
- Adaptar respostas conforme as decisões anteriores do jogador

---

## 🧩 Arquitetura do Sistema

Fluxo geral:

1. A Unity captura a ação do jogador.
2. A Unity envia uma requisição JSON para a API Python.
3. A API converte essa requisição em um prompt e encaminha para o LLM.
4. O LLM retorna dados narrativos estruturados.
5. A API valida e devolve o conteúdo para a Unity.
6. A Unity atualiza a interface e o estado do jogo.

---

## 🛠️ Tecnologias

### **Unity**
- C#
- Motor Unity

### **Backend**
- Python 3.x
- Flask
- Comunicação JSON/HTTP

### **Geração Procedural**
- Geração determinística de masmorras
- Regras de compatibilidade entre salas (tile-based)
- Geração de eventos orientada por IA

---

## 🎮 Funcionalidades

### **Layout de Masmorra Procedural**
A masmorra é montada dinamicamente usando:
- Posicionamento recursivo de salas
- Restrições de conectividade
- Escalonamento por nível de dificuldade

### **Narrativa Guiada por IA**
O LLM produz:
- Descrições das cenas
- Opções de diálogo
- Itens, eventos e personagens
- Progressão narrativa baseada no contexto

### **Geração de Eventos**
Ao entrar em uma nova sala, podem ocorrer:
- Criação de inimigos
- Geração de itens
- Descrições ambientais
- Sequências de diálogo com NPCs

### **Sistema de Diálogo**
As entradas do jogador são enviadas para a API, e o LLM retorna respostas consistentes com o estado atual do jogo.

---

## 🧪 Executando o Projeto

### **Unity**
1. Abra a pasta `Unity/` pelo Unity Hub.
2. Carregue a cena principal.
3. Pressione **Play**.

### **API Python**

Instale as dependências:

```bash
pip install -r requirements.txt
```

Run the API

```bash
  flask run
```