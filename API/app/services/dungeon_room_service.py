import json

from openai import OpenAI
from app.utils.schema_util import load_json_schema

client = OpenAI()

def generate_dungeon_room(selected_model: str, context: str):
    dungeon_room_system = "You are an imaginative Dungeon Master."
    dungeon_room_user = f"""
    Generate a dungeon room based on: {context}.

    Randomize the number of enemies using:
    - 15%: no enemies
    - 30%: 1 enemy
    - 35%: 2 enemies
    - 20%: 3 enemies

    Keep the room immersive and consistent with the dungeon theme, but keep the final description extremely concise: 
    2–4 sentences maximum, only highlighting the room’s key features and immediate atmosphere. 
    Avoid long paragraphs or unnecessary detail.
    """

    dungeon_room_schema = load_json_schema("dungeon_room_schema.json")
    response = client.responses.create(
        model=selected_model,
        input=[
            {"role": "system", "content": dungeon_room_system},
            {"role": "user", "content": dungeon_room_user},
        ],
        text=dungeon_room_schema,
    )
    return json.loads(response.output_text)
