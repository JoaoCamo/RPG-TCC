from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()


def generate_dungeon_room():
    dungeon_room_system = "You are an imaginative Dungeon Master."
    dungeon_room_user = "Generate this dungeon room based on the dungeon theme."
    dungeon_room_schema = load_json_schema("dungeon_room_schema.json")
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": dungeon_room_system},
            {"role": "user", "content": dungeon_room_user},
        ],
        text=dungeon_room_schema,
    )
    return response.output_text
