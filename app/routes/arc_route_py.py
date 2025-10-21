from flask import Blueprint, request, jsonify
from app.services.arc_service import generate_arc

arc_blueprint = Blueprint("arc", __name__)

@arc_blueprint.route("/", methods=["POST"])
def arc():
    data = request.get_json()
    last_action = data.get("lastPlayerAction")
    current_arc = data.get("currentArc")
    is_ending = data.get("isEnding")
    arc = generate_arc(last_action, current_arc, is_ending)
    return jsonify(arc)