import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()

def generate_item(character: str, item_type: str):
    item_system = "You are an imaginative Dungeon Master."
    item_user = f"""
                    Generate a {item_type} based on the character that will drop it: {character}.
                    
                    Ensure that the item is appropriate for the character's level, abilities, and personality. 
                    The item should feel natural as a drop from this character, matching their style, skills, and story context.
                    
                    Include details such as:
                        - Name of the item
                        - Description and lore
                        - Any special abilities, effects, or stats
                    
                    The item should be creative, immersive, and consistent with the character's theme.
                 """

    if item_type == "Weapon":
        item_schema = load_json_schema("weapon_schema.json")
    else:
        item_schema = load_json_schema("armor_schema.json")

    response = client.responses.create(
        model="gpt-4.1",
        input=[
            {"role": "system", "content": item_system},
            {"role": "user", "content": item_user},
        ],
        text=item_schema,
    )
    return json.loads(response.output_text)