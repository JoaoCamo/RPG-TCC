import json

from openai import OpenAI
from app.utils.schema_util import load_json_schema

client = OpenAI()

def generate_dungeon_room(context: str):
    dungeon_room_system = "You are an imaginative Dungeon Master."
    dungeon_room_user = f"""
                            Generate a dungeon room based on the following context: {context}.
                            
                            Randomize the number of enemies present in the room according to these probabilities:
                                - 15% chance of having no enemies
                                - 30% chance of having 1 enemy
                                - 35% chance of having 2 enemies
                                - 20% chance of having 3 enemies
                            
                            Ensure the dungeon room feels immersive, balanced, and consistent with the context of the dungeon.
                         """
    dungeon_room_schema = load_json_schema("dungeon_room_schema.json")
    response = client.responses.create(
        model="gpt-4.1",
        input=[
            {"role": "system", "content": dungeon_room_system},
            {"role": "user", "content": dungeon_room_user},
        ],
        text=dungeon_room_schema,
    )
    return json.loads(response.output_text)
