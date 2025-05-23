from openai import OpenAI

client = OpenAI()


def generate_dungeon_room(layout):
    dungeon_room_system = "Help pick a place for the final room of the dungeon."
    dungeon_room_user = f"Based on this layout: {layout}\nChoose a place among the 1's(ones) and change it to a 3, dont change any of the 0's or the 2. Return ONLY the changed list without anything else."
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": dungeon_room_system},
            {"role": "user", "content": dungeon_room_user},
        ],
    )
    return response.output_text
