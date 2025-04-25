from openai import OpenAI

client = OpenAI()


def generate_dungeon():
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[{"role": "system", "content": ""}, {"role": "user", "content": ""}],
    )
    return response
