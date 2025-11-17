import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()

def generate_item(selected_model: str, character: str, item_type: str):
    item_system = "You are an imaginative Dungeon Master."
    item_user = f"""
        Generate a {item_type} that would naturally be dropped by the following character: {character}.

        Items must follow official 5e conventions for rarity, power, balance, and rules.

        Armor must follow standard 5e AC ranges:
            - Light Armor: 11–12
            - Medium Armor: 12–15
            - Heavy Armor: 14–18
            - Shields: always +2 AC

        Weapons must use official 5e damage dice and proper weapon properties only.

        Descriptions must be extremely concise: 1–2 short sentences, focusing only on key visual or thematic elements. Avoid long lore or detailed explanations.

        The character's level is extremely important when designing the item.
        Low-level or weak creatures should only drop simple, modest, or flavorful items.
        Mid-level foes may drop more impactful but still balanced items.
        High-level bosses, extraplanar beings, divine entities, or legendary characters can justify rare and powerful items, still within 5e limits.

        The item must feel thematically appropriate for {character}, reflecting their abilities, personality, and narrative role.

        The item must use as its ModifierStat the character's highest ability score
        (e.g., if the character's best stat is Intelligence, the item must use Intelligence;
        if it's Strength, use Strength; and so on). Never produce items with a ModifierStat
        that isn't the character's best stat.

        Never generate items with 0 dice to roll.
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