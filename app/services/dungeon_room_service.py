from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()


def generate_dungeon_layout():
    dungeon_layout_system = "You are an imaginative Dungeon Master."
    dungeon_layout_user = "Generate this dungeon layout based on the dungeon theme, the dungeon layout has a 50% chance to not have any enemies present, 25% chance to have 1 enemy present, 12.5% chance to have 2 enemies present and 12.5% chance to have 3 enemies present."
    dungeon_layout_schema = load_json_schema("dungeon_layout_schema.json")
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": dungeon_layout_system},
            {"role": "user", "content": dungeon_layout_user},
        ],
        text=dungeon_layout_schema,
    )
    return response.output_text
