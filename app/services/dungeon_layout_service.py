from openai import OpenAI

client = OpenAI()


def generate_dungeon_layout(layout):
    dungeon_layout_system = "Help pick a place for the final layout of the dungeon."
    dungeon_layout_user = f"Based on this layout: {layout}\nChoose a place among the 1's(ones) and change it to a 3, dont change any of the 0's or the 2. Return ONLY the changed list without anything else."
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": dungeon_layout_system},
            {"role": "user", "content": dungeon_layout_user},
        ],
    )
    return response.output_text
