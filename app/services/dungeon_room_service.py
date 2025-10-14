import json

from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()


def generate_dungeon_room(context: str):
    dungeon_room_system = "You are an imaginative Dungeon Master."
    dungeon_room_user = f"Generate this dungeon room based on {context}, the dungeon room has a 15% chance to not have any enemies present, 30% chance to have 1 enemy present, 35% chance to have 2 enemies present and 20% chance to have 3 enemies present."
    dungeon_room_schema = load_json_schema("dungeon_room_schema.json")
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": dungeon_room_system},
            {"role": "user", "content": dungeon_room_user},
        ],
        text=dungeon_room_schema,
    )
    return json.loads(response.output_text)
