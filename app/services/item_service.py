import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()

def generate_item(selected_model: str, character: str, item_type: str):
    item_system = "You are an imaginative Dungeon Master."
    item_user = f"""
    Generate a {item_type} that would naturally be dropped by the following character: {character}.

    Items must follow established 5e conventions for rarity, power scaling, and rules.
    Armor must use correct AC progression for light, medium, and heavy armor, as well as shields. 
    Weapons must only use official damage dice and weapon properties

    Also take into account the level, power, and nature of {character}. 
    Low-level or weak creatures should not drop overly powerful or world-breaking items; their loot should be simple, modest, 
    or thematically flavorful. Mid-level foes may drop more impactful but still balanced items. 
    High-level bosses, extraplanar beings, divine entities, or legendary characters can justify rare or exceptionally powerful items.

    The item should feel thematically appropriate for {character}, reflecting their abilities, personality, and narrative role.
    """

    if item_type == "Weapon":
        item_schema = load_json_schema("weapon_schema.json")
    else:
        item_schema = load_json_schema("armor_schema.json")

    response = client.responses.create(
        model=selected_model,
        input=[
            {"role": "system", "content": item_system},
            {"role": "user", "content": item_user},
        ],
        text=item_schema,
    )
    return json.loads(response.output_text)