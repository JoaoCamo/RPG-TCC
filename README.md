<div align="center">

# 🌐 Documentation / Documentação

</div>

---

## **English**

<details>
<summary><strong>Show English documentation</strong></summary>

<br>

# Procedural Narrative System

This repository contains the prototype developed for the **Undergraduate Final Project**.  
The project integrates Unity, a Python REST API, and a large-scale language model (LLM) to generate procedural narrative content, events, and dialogues during gameplay.

---

## 🚀 System Overview

The project is divided into three components:

### **1. Unity Client (C#)**
Handles:
- User interface and interaction
- Display of narrative text and choices
- Procedural dungeon generation
- Combat, exploration, and inventory systems
- HTTP communication with the API

### **2. Python API**
Responsible for:
- Receiving game context and player actions
- Building prompts dynamically
- Sending structured requests to the LLM
- Returning standardized JSON responses to Unity

### **3. Large-Scale Language Model (LLM)**
Used to:
- Generate narrative text and dialogue options
- Produce contextual descriptions, events, items, and entities
- Adapt responses according to previous player decisions

---

## 🧩 System Architecture

General workflow:

1. Unity captures a player action.
2. Unity sends a JSON request to the Python API.
3. The API converts the request into a prompt and forwards it to the LLM.
4. The LLM returns structured narrative data.
5. The API validates and returns the data to Unity.
6. Unity updates the interface and game state.

---

## 🛠️ Technologies

### **Unity**
- C#
- Unity game engine

### **Backend**
- Python 3.x
- Flask
- JSON/HTTP communication

### **Procedural Generation**
- Deterministic dungeon layout generation
- Tile-based room compatibility rules
- AI-based event generation

---

## 🎮 Features

### **Procedural Dungeon Layout**
The dungeon map is assembled dynamically.  
Generation uses:
- Recursive room placement
- Connectivity constraints
- Difficulty-based scaling

### **AI-Driven Narrative**
The LLM outputs:
- Descriptions of scenes
- Dialogue options
- Items, events, and characters
- Context-based narrative progression

### **Event Generation**
Entering a new room may trigger:
- Enemy creation
- Item allocation
- Environmental descriptions
- NPC dialogue sequences

### **Dialogue System**
Player inputs are sent to the API, and the LLM returns responses consistent with the current game state.

---

## 🧪 Running the Project

### **Unity**
1. Open the `Unity/` folder in Unity Hub.
2. Load the main scene.
3. Press **Play**.

### **Python API**

Install dependencies:
```bash
  pip install -r requirements.txt
```

Run the API

```bash
  flask run
```

</details>

---

## **Português**

<details>
<summary><strong>Mostrar conteúdo em Português</strong></summary>

<br>

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

</details>
